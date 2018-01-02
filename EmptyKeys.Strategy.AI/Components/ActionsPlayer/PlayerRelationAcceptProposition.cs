using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Diplomacy;
using System;
using System.Linq;

namespace EmptyKeys.Strategy.AI.Components.ActionsPlayer
{

    /// <summary>
    /// Implements player action for behavior. This action accepts diplomatic proposition by other Player.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class PlayerRelationAcceptProposition : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerRelationAcceptProposition"/> class.
        /// </summary>
        public PlayerRelationAcceptProposition()
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
            if (otherPlayer == null) // || relation.DeclarationCooldown != 0)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            if (otherPlayer.IsEliminated)
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            var otherPlayerRelation = otherPlayer.RelationsValues.FirstOrDefault(r => playerContext.Player.Index == r.PlayerIndex);
            if (otherPlayerRelation.PropositionState != DiplomaticActions.None)
            {                
                DispatcherHelper.InvokeOnMainThread(otherPlayer, new Action(() =>
                {
                    otherPlayer.ActivateProposedDiplomaticAction(otherPlayerRelation);
                }));

                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
