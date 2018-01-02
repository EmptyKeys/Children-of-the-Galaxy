using System.Linq;
using System.Xml.Serialization;
using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Diplomacy;
using EmptyKeys.Strategy.Diplomacy.Configuration;
using EmptyKeys.Strategy.Trade;

namespace EmptyKeys.Strategy.AI.Components.ActionsPlayer
{
    /// <summary>
    /// Implements player action for behavior. This action updates diplomatic relation with other player.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class PlayerUpdateRelationsValue : BehaviorComponentBase
    {
        /// <summary>
        /// Gets or sets the influence relation change.
        /// </summary>
        /// <value>
        /// The influence relation change.
        /// </value>
        [XmlAttribute]
        public int InfluenceRelationChange { get; set; }

        /// <summary>
        /// Gets or sets the technology relation change.
        /// </summary>
        /// <value>
        /// The technology relation change.
        /// </value>
        [XmlAttribute]
        public int TechnologyRelationChange { get; set; }

        /// <summary>
        /// Gets or sets the global trade relation change.
        /// </summary>
        /// <value>
        /// The global trade relation change.
        /// </value>
        [XmlAttribute]
        public int GlobalTradeRelationChange { get; set; }

        /// <summary>
        /// Gets or sets the open borders relation change.
        /// </summary>
        /// <value>
        /// The open borders relation change.
        /// </value>
        [XmlAttribute]
        public int OpenBordersRelationChange { get; set; }

        /// <summary>
        /// Gets or sets the close borders relation change.
        /// </summary>
        /// <value>
        /// The close borders relation change.
        /// </value>
        [XmlAttribute]
        public int CloseBordersRelationChange { get; set; }

        /// <summary>
        /// Gets or sets the open embassy relation change.
        /// </summary>
        /// <value>
        /// The open embassy relation change.
        /// </value>
        [XmlAttribute]
        public int OpenEmbassyRelationChange { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerUpdateRelationsValue"/> class.
        /// </summary>
        public PlayerUpdateRelationsValue()
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
            DiplomacyConfiguration diplomacyConfig = player.GameSession.EnvironmentConfig.DiplomacyConfig;
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

            float otherPlayerInfluence = player.GameSession.Galaxy.PlayersInfluence[otherPlayer];
            float myInfluence = player.GameSession.Galaxy.PlayersInfluence[player];
            if (otherPlayerInfluence > myInfluence)
            {
                relation.UpdateLevel(relation.RelationValue - InfluenceRelationChange, diplomacyConfig);
            }
            else
            {
                relation.UpdateLevel(relation.RelationValue + InfluenceRelationChange, diplomacyConfig);
            }

            if (otherPlayer.TotalTechnologiesAquired > player.TotalTechnologiesAquired)
            {
                relation.UpdateLevel(relation.RelationValue + TechnologyRelationChange, diplomacyConfig);
            }

            int tradeRoutesWithPlayer = player.TradeRoutes.Count(tr => tr is GlobalTradeRoute && (tr.From.Owner == otherPlayer || tr.To.Owner == otherPlayer));
            if (tradeRoutesWithPlayer > 0)
            {
                int relationChange = GlobalTradeRelationChange * tradeRoutesWithPlayer;
                relation.UpdateLevel(relation.RelationValue + relationChange, diplomacyConfig);
            }

            if (relation.Player.HasOpenBorders(player))
            {
                relation.UpdateLevel(relation.RelationValue + OpenBordersRelationChange, diplomacyConfig);
            }
            else
            {
                relation.UpdateLevel(relation.RelationValue + CloseBordersRelationChange, diplomacyConfig);
            }

            if (relation.Player.HasOpenEmbassy(player))
            {
                relation.UpdateLevel(relation.RelationValue + OpenEmbassyRelationChange, diplomacyConfig);                
            }

            if (relation.Player.IsAtWar(player))
            {
                relation.UpdateLevel(diplomacyConfig.HostileRelationValueMin, diplomacyConfig);
            }

            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
