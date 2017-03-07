namespace EmptyKeys.Strategy.AI.Components.Decorators
{
    /// <summary>
    /// Implements player core decorator for behavior. This decorator executes race specific Core behavior (if there is any).
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.Decorators.BehaviorDecoratorBase" />
    public class PlayerCoreBehavior : BehaviorDecoratorBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerTradeBehavior"/> class.
        /// </summary>
        public PlayerCoreBehavior()
            : base()
        {
        }

        /// <summary>
        /// Executes the behavior with given context
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
            
            Behavior = playerContext.Player.Race.CoreBehavior;
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
