using System.Xml.Serialization;

namespace EmptyKeys.Strategy.AI.Components.ActionsPlanet
{
    /// <summary>
    /// Implements planet action for behavior. This action sets behavior for planet.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class PlanetSetBehavior : BehaviorComponentBase
    {
        /// <summary>
        /// Gets or sets the name of the behavior.
        /// </summary>
        /// <value>
        /// The name of the behavior.
        /// </value>
        [XmlAttribute]
        public string BehaviorName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlanetSetBehavior"/> class.
        /// </summary>
        public PlanetSetBehavior()
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

            Behavior behavior = null;
            if (!string.IsNullOrEmpty(BehaviorName))
            {
                behavior = BehaviorsManager.Instance.GetBehavior(BehaviorName);
                if (behavior == null)
                {
                    returnCode = BehaviorReturnCode.Failure;
                    return returnCode;
                }
            }

            planetContext.Planet.Behavior = behavior;

            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
