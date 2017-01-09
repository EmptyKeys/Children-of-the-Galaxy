using System;
using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Diplomacy;

namespace EmptyKeys.Strategy.AI.Components.ActionsPlayer
{
    /// <summary>
    /// Implements player action for behavior. This action declares war to other Player.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class PlayerRelationDeclareWar : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerRelationDeclareWar"/> class.
        /// </summary>
        public PlayerRelationDeclareWar()
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
            if (otherPlayer == null || relation.DeclarationCooldown != 0)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            if (otherPlayer.IsEliminated)
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            if (!relation.IsAtWar)
            {
                DispatcherHelper.InvokeOnMainThread(relation.Player, new Action(() =>
                {
                    player.DeclareWar(relation);
                }));

                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
