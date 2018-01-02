using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Diplomacy;
using System;
using System.Linq;
using System.Xml.Serialization;

namespace EmptyKeys.Strategy.AI.Components.ActionsPlayer
{

    /// <summary>
    /// Implements player action for behavior. This action proposes diplomatic action to other Player.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class PlayerRelationProposeAction : BehaviorComponentBase
    {
        /// <summary>
        /// Gets or sets the diplomatic action.
        /// </summary>
        /// <value>
        /// The diplomatic action.
        /// </value>
        [XmlAttribute]
        public DiplomaticActions DiplomaticAction { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerRelationProposeAction"/> class.
        /// </summary>
        public PlayerRelationProposeAction()
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
            if (player == null || relation.DeclarationCooldown != 0 || relation.PropositionState != DiplomaticActions.None || !relation.AvailableActions.HasFlag(DiplomaticAction))
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            Player otherPlayer = relation.Player;
            if (otherPlayer == null || otherPlayer.IsEliminated)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            bool currentState = relation.GetActionState(DiplomaticAction);
            if (currentState)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            bool canActivate = relation.GetDiplomaticActionEnableState(DiplomaticAction, player.GameSession.EnvironmentConfig.DiplomacyConfig);
            if (canActivate)
            {                
                DispatcherHelper.InvokeOnMainThread(otherPlayer, new Action(() =>
                {
                    player.ProposeDiplomaticAction(relation, DiplomaticAction);
                }));

                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
