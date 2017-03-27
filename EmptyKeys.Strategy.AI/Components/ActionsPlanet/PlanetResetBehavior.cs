using System.Xml.Serialization;

namespace EmptyKeys.Strategy.AI.Components.ActionsPlanet
{
    /// <summary>
    /// Implements planet action for behavior. This action resets behavior of the planet.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class PlanetResetBehavior : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlanetResetBehavior"/> class.
        /// </summary>
        public PlanetResetBehavior()
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

            planetContext.Planet.BehaviorName = string.Empty;
            planetContext.Planet.Behavior = null;

            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
