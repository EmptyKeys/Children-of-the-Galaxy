namespace EmptyKeys.Strategy.AI.Components.ConditionsUnit
{
    /// <summary>
    /// Implements unit condition for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class UnitCanColonize : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitCanColonize"/> class.
        /// </summary>
        public UnitCanColonize()
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
            
            if (unitContext.Unit.CanColonize)
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
