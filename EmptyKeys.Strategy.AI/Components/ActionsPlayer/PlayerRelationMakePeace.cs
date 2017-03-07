using System;
using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Diplomacy;

namespace EmptyKeys.Strategy.AI.Components.ActionsPlayer
{
    /// <summary>
    /// Implements player action for behavior. This action makes peace with other Player.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class PlayerRelationMakePeace : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerRelationDeclareWar"/> class.
        /// </summary>
        public PlayerRelationMakePeace()
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
            
            if (relation.IsAtWar)
            {
                float cost = player.GameSession.EnvironmentConfig.DiplomacyConfig.GetActionCost(DiplomaticActions.MakePeace);
                if (cost > player.Intelligence)
                {
                    returnCode = BehaviorReturnCode.Failure;
                    return returnCode;
                }

                DispatcherHelper.InvokeOnMainThread(relation.Player, new Action(() =>
                {
                    player.MakePeace(relation);
                }));

                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
