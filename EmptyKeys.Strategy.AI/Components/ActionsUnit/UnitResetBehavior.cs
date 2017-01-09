namespace EmptyKeys.Strategy.AI.Components.Actions
{
    /// <summary>
    /// Implements action, which resets behavior of the unit.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class UnitResetBehavior : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitResetBehavior"/> class.
        /// </summary>
        public UnitResetBehavior()
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

            unitContext.Unit.BehaviorName = string.Empty;
            unitContext.Unit.Behavior = null;
            unitContext.EnvironmentTarget = null;            

            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
