using System.Linq;
using System.Xml.Serialization;
using EmptyKeys.Strategy.Environment;
using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.ConditionsPlanet
{
    /// <summary>
    /// Implements planet condition for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class PlanetHasEnoughDefense : BehaviorComponentBase
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
        /// Initializes a new instance of the <see cref="PlanetHasEnoughDefense"/> class.
        /// </summary>
        public PlanetHasEnoughDefense()
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

            BaseEnvironment planetEnvironment = planetContext.Planet.Environment;
            BaseUnit unit = planetContext.Planet.Owner.Units.FirstOrDefault(u => u.Environment == planetEnvironment && u is MoveableUnit && !((MoveableUnit)u).IsOnOrbit && u.BehaviorName == DefensiveBehaviorName);
            if (unit != null)
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
