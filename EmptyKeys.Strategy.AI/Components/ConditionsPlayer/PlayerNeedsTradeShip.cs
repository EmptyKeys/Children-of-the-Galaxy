using System.Linq;
using System.Xml.Serialization;
using EmptyKeys.Strategy.Environment;
using EmptyKeys.Strategy.Trade;
using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.ConditionsPlayer
{
    /// <summary>
    /// Implements player condition for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class PlayerNeedsTradeShip : BehaviorComponentBase
    {
        /// <summary>
        /// Gets or sets the route ship ratio.
        /// </summary>
        /// <value>
        /// The route ship ratio.
        /// </value>
        [XmlAttribute]
        public float RouteShipRatio { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerNeedsTradeShip"/> class.
        /// </summary>
        public PlayerNeedsTradeShip()
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
            
            var tradePoints  = playerContext.Player.StarSystemBodies.Where(b => b is ITradePoint && ((ITradePoint)b).TradeRoutesCount > 0);
            if (tradePoints == null || tradePoints.Count() == 0)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            int buildingShipCount = 0;
            foreach (var body in playerContext.Player.StarSystemBodies)
            {
                Planet planet = body as Planet;
                if (planet == null)
                {
                    continue;
                }

                buildingShipCount += planet.FactoryQueue.Count(i => i.Item.UnitConfig.Actions.HasFlag(UnitActions.EstablishTradeRoute));
            }

            int tradeRouteSlotsCount = (int)(tradePoints.Sum(tp => ((ITradePoint)tp).TradeRoutesCount) * RouteShipRatio);
            int tradeShipsCount = playerContext.Player.Units.Count(u => u is ITradeUnit);
            int totalShipsCount = tradeShipsCount + buildingShipCount;

            if (tradeRouteSlotsCount > totalShipsCount && playerContext.Player.TradeShipsLimit > totalShipsCount)
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
