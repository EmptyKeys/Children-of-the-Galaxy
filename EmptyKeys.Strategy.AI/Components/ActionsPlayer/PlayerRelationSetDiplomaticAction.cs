using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Diplomacy;
using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace EmptyKeys.Strategy.AI.Components.ActionsPlayer
{
    /// <summary>
    /// Implements player action for behavior. This sets diplomatic action to some state.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class PlayerRelationSetDiplomaticAction : BehaviorComponentBase
    {
        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        [XmlAttribute]
        public DiplomaticActions Action { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether diplomatic action should be set to active or not.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active true; otherwise, <c>false</c>.
        /// </value>
        [XmlAttribute]
        public bool State { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [check cost].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [check cost]; otherwise, <c>false</c>.
        /// </value>
        [XmlAttribute]
        public bool IsProposition { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerRelationSetDiplomaticAction"/> class.
        /// </summary>
        public PlayerRelationSetDiplomaticAction()
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
            /*
            if (!relation.AvailableActions.HasFlag(Action))
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }
            */

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

            bool currentState = relation.GetActionState(Action);
            if (currentState == State || !relation.AvailableActions.HasFlag(Action))
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            if (Action == DiplomaticActions.BordersControl && relation.IsEntryAllowed == State)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            bool canActivate = relation.GetDiplomaticActionEnableState(Action, player.GameSession.EnvironmentConfig.DiplomacyConfig);
            if (canActivate || IsProposition)
            {
                if (!IsProposition)
                {
                    float cost = player.GameSession.EnvironmentConfig.DiplomacyConfig.GetActionCost(Action);
                    if (cost > player.Intelligence)
                    {
                        returnCode = BehaviorReturnCode.Failure;
                        return returnCode;
                    }
                }

                if (State)
                {
                    DispatcherHelper.InvokeOnMainThread(relation.Player, new Action(() =>
                    {
                        player.ActivateDiplomaticAction(relation, Action, !IsProposition);
                    }));
                }
                else
                {
                    DispatcherHelper.InvokeOnMainThread(relation.Player, new Action(() =>
                    {
                        player.DeactivateDiplomaticAction(relation, Action, !IsProposition);
                    }));
                }

                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
