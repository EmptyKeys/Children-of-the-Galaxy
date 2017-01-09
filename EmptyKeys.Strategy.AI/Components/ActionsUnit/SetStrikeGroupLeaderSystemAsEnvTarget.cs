namespace EmptyKeys.Strategy.AI.Components.Actions
{
    /// <summary>
    /// Implements unit action for behavior. This actions sets EnvironmentTarget to strike group leader Environment.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class SetStrikeGroupLeaderSystemAsEnvTarget : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetStrikeGroupLeaderSystemAsEnvTarget"/> class.
        /// </summary>
        public SetStrikeGroupLeaderSystemAsEnvTarget()
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

            unitContext.EnvironmentTarget = unitContext.Unit.StrikeGroup.GroupLeader.Environment;

            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
