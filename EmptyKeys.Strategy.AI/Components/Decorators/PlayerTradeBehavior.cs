namespace EmptyKeys.Strategy.AI.Components.Decorators
{
    /// <summary>
    /// Implements player trade decorator for behavior. This decorator executes race specific Trade behavior (if there is any).
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.Decorators.BehaviorDecoratorBase" />
    public class PlayerTradeBehavior : BehaviorDecoratorBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerTradeBehavior"/> class.
        /// </summary>
        public PlayerTradeBehavior()
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
            
            Behavior = playerContext.Player.Race.TradeBehavior;
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
