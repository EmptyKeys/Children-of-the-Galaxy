namespace EmptyKeys.Strategy.AI.Components.Decorators
{
    /// <summary>
    /// Implements player research decorator for behavior. This decorator executes race specific Research behavior (if there is any).
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.Decorators.BehaviorDecoratorBase" />
    public class PlayerResearchBehavior : BehaviorDecoratorBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerResearchBehavior"/> class.
        /// </summary>
        public PlayerResearchBehavior()
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
            if (playerContext == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }
            
            Behavior = playerContext.Player.Race.ResearchBehavior;
            if (Behavior == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            returnCode = Behavior.Behave(context);            
            return returnCode;
        }
    }
}
