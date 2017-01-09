namespace EmptyKeys.Strategy.AI.Components.ActionsPlayer
{
    /// <summary>
    /// Implements player action for behavior. This action is using enumerator and gets next relation value.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class PlayerNextPlayerRelations : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerNextPlayerRelations"/> class.
        /// </summary>
        public PlayerNextPlayerRelations()
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
            PlayerBehaviorContext playerContext = context as PlayerBehaviorContext;
            if (playerContext == null || playerContext.RelationValues == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            if (!playerContext.RelationValues.MoveNext())
            {
                playerContext.RelationValues.Dispose();
                playerContext.RelationValues = null;
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }
           
            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
