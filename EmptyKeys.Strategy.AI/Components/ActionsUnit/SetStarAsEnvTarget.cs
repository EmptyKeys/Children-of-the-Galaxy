using EmptyKeys.Strategy.Environment;

namespace EmptyKeys.Strategy.AI.Components.Actions
{
    /// <summary>
    /// Implements unit action for behavior. This action gets StarSystem from EnvironmentTarget and sets EnvironmentTarget to BaseStar of that system.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class SetStarAsEnvTarget : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetStarAsEnvTarget"/> class.
        /// </summary>
        public SetStarAsEnvTarget()
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

            StarSystem system = unitContext.Unit.Environment as StarSystem;
            if (system == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            unitContext.EnvironmentTarget = system.BaseStar;
            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
