using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.Conditions
{
    /// <summary>
    /// Implements unit condition for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class StrikeGroupLeaderMiniJumped : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StrikeGroupLeaderMiniJumped"/> class.
        /// </summary>
        public StrikeGroupLeaderMiniJumped()
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

            MoveableUnit leader = unitContext.Unit.StrikeGroup.GroupLeader as MoveableUnit;
            if (leader != null && leader.IsMiniJumping())
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
