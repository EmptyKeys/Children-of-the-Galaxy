using System;
using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Diplomacy;

namespace EmptyKeys.Strategy.AI.Components.ActionsPlayer
{
    /// <summary>
    /// Implements player action for behavior. This action opens borders to other Player.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class PlayerRelationOpenBorders : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerRelationOpenBorders"/> class.
        /// </summary>
        public PlayerRelationOpenBorders()
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
            PlayerRelationValue relation = playerContext.RelationValues.Current;
            Player otherPlayer = relation.Player;
            if (otherPlayer == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            if (otherPlayer.IsEliminated)
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            if (!relation.IsEntryAllowed)
            {
                DispatcherHelper.InvokeOnMainThread(relation.Player, new Action(() =>
                {
                    player.OpenBorders(relation);                    
                }));
            }

            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
