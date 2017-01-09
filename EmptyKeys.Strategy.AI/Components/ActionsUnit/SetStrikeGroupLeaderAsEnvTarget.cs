namespace EmptyKeys.Strategy.AI.Components.Actions
{
    /// <summary>
    /// Implements unit action for behavior. This action sets EnvironmentTarget of UnitBehaviorContext to GroupLeader of strike group.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class SetStrikeGroupLeaderAsEnvTarget : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetStrikeGroupLeaderAsEnvTarget"/> class.
        /// </summary>
        public SetStrikeGroupLeaderAsEnvTarget()
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
            if (unitContext == null || unitContext.Unit == null || unitContext.Unit.StrikeGroup == null || unitContext.Unit.StrikeGroup.GroupLeader == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            unitContext.EnvironmentTarget = unitContext.Unit.StrikeGroup.GroupLeader;

            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
