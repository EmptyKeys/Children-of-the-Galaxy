using EmptyKeys.Strategy.Environment;

namespace EmptyKeys.Strategy.AI.Components.ConditionsUnit
{
    /// <summary>
    /// Implements unit condition for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class StrikeGroupLeaderIsInGalaxy : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StrikeGroupLeaderIsInGalaxy"/> class.
        /// </summary>
        public StrikeGroupLeaderIsInGalaxy()
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
            
            if (unitContext.Unit.StrikeGroup.GroupLeader.Environment is Galaxy)
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
