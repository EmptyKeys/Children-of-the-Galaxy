using EmptyKeys.Strategy.Core;

namespace EmptyKeys.Strategy.AI.Components.ConditionsPlayer
{
    /// <summary>
    /// Implements player condition for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class PlayerHasEnoughResources : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerHasEnoughResources"/> class.
        /// </summary>
        public PlayerHasEnoughResources()
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

            Player player = playerContext.Player;
            foreach (var item in player.AvailFactoryItems)
            {
                if (item.RareResourceCost == null || item.RareResourceCost.Count == 0)
                {
                    continue;
                }

                if (item.HasEnoughRareResource(player))
                {
                    continue;
                }

                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
