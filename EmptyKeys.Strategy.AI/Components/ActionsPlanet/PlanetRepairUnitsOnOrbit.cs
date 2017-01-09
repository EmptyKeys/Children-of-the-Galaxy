using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using EmptyKeys.Strategy.Units;
using EmptyKeys.Strategy.Units.Tasks;

namespace EmptyKeys.Strategy.AI.Components.ActionsPlanet
{
    /// <summary>
    /// Implements planet action for behavior. This action repairs any unit on orbit, which has hull bellow HullPercentage.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class PlanetRepairUnitsOnOrbit : BehaviorComponentBase
    {
        /// <summary>
        /// Gets or sets the hull percentage.
        /// </summary>
        /// <value>
        /// The hull percentage.
        /// </value>
        [XmlAttribute]
        public float HullPercentage { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlanetRepairUnitsOnOrbit"/> class.
        /// </summary>
        public PlanetRepairUnitsOnOrbit()
            : base()
        {
            HullPercentage = 1;
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
            foreach (var unit in units)
            {
                if (unit.Hull == unit.HullMax || unit.Hull > unit.HullMax * HullPercentage || unit.IsDead || unit.Owner == null)
                {
                    continue;
                }
                
                if (unit.Owner.Energy < unit.RepairCost)
                {
                    returnCode = BehaviorReturnCode.Failure;
                    return returnCode;
                }

                RepairOnOrbitTask task = new RepairOnOrbitTask(unit);
                task.Execute();

                if (!task.IsTaskFinished)
                {
                    returnCode = BehaviorReturnCode.Failure;
                    return returnCode;
                }
            }

            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
