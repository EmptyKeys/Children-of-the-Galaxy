namespace EmptyKeys.Strategy.AI.Components.Conditions
{
    /// <summary>
    /// Implements unit condition for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class StrikeGroupLeaderIsInSameSystem : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StrikeGroupLeaderIsInSameSystem"/> class.
        /// </summary>
        public StrikeGroupLeaderIsInSameSystem()
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
            if (unitContext == null || unitContext.Unit == null || unitContext.Unit.StrikeGroup == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }
            
            if (unitContext.Unit.StrikeGroup.GroupLeader.Environment == unitContext.Unit.Environment)
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
