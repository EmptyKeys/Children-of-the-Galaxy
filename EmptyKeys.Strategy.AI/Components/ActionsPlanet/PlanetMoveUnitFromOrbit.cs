using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using EmptyKeys.Strategy.Trade;
using EmptyKeys.Strategy.Units;
using EmptyKeys.Strategy.Units.Tasks;

namespace EmptyKeys.Strategy.AI.Components.ActionsPlanet
{
    /// <summary>
    /// Implements planet action for behavior. This action moves any unit on orbit to star system.
    /// Based on DefensiveRatio some units are set to defense by setting behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class PlanetMoveUnitFromOrbit : BehaviorComponentBase
    {
        /// <summary>
        /// Gets or sets the name of the defensive behavior.
        /// </summary>
        /// <value>
        /// The name of the defensive behavior.
        /// </value>
        [XmlAttribute]
        public string DefensiveBehaviorName { get; set; }

        /// <summary>
        /// Gets or sets the defensive ratio.
        /// </summary>
        /// <value>
        /// The defensive ratio.
        /// </value>
        [XmlAttribute]
        public int DefensiveRatio { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlanetMoveUnitFromOrbit"/> class.
        /// </summary>
        public PlanetMoveUnitFromOrbit()
            : base()
        {
        }

        /// <summary>
        /// Executes behavior with given context
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override BehaviorReturnCode Behave(IBehaviorContext context)
        {
            PlanetBehaviorContext planetContext = context as PlanetBehaviorContext;
            if (planetContext == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            if (!planetContext.Planet.Orbit.Units.ContainsKey(planetContext.Planet.Owner))
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            // TODO MT issue here?
            List<MoveableUnit> units = planetContext.Planet.Orbit.Units[planetContext.Planet.Owner].ToList();
            int defensiveUnitsCount = 0;
            int defensiveForce = 0;
            if (DefensiveRatio > 0)
            {
                defensiveForce = planetContext.Planet.Owner.MaxUnitsOnOrbit / DefensiveRatio;
            }

            foreach (var unit in units)
            {
                if (!string.IsNullOrEmpty(DefensiveBehaviorName) && unit.BehaviorName == DefensiveBehaviorName)
                {
                    if (defensiveForce > defensiveUnitsCount)
                    {
                        defensiveUnitsCount++;
                        continue;
                    }
                }

                if (unit is ITradeUnit && ((ITradeUnit)unit).HasAssignedTradeRoute)
                {
                    continue;
                }

                LeaveOrbitTask task = new LeaveOrbitTask(unit, planetContext.Planet);
                task.Execute();

                if (task.IsTaskFinished)
                {
                    returnCode = BehaviorReturnCode.Success;
                    return returnCode;
                }
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
