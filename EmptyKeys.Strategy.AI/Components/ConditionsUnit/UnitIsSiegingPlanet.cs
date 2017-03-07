using EmptyKeys.Strategy.Environment;

namespace EmptyKeys.Strategy.AI.Components.ConditionsUnit
{
    /// <summary>
    /// Implements unit condition for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class UnitIsSiegingPlanet : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitIsSiegingPlanet"/> class.
        /// </summary>
        public UnitIsSiegingPlanet()
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
            UnitBehaviorContext unitContext = context as UnitBehaviorContext;
            if (unitContext == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            Planet siegingPlanet = unitContext.Unit.GetOrbitingPlanet();
            if (siegingPlanet == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            if (siegingPlanet.IsUnderSiege && siegingPlanet.Owner != unitContext.Unit.Owner)
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
