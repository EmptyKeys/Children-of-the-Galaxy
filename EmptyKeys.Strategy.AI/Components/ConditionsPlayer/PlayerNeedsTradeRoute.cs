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
    public class PlayerNeedsTradeRoute : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerNeedsTradeShip"/> class.
        /// </summary>
        public PlayerNeedsTradeRoute()
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
            if (tradePoints.Count() == 0)
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            int sum = tradePoints.Sum(tp => ((ITradePoint)tp).TradeRoutesCount);
            if (sum <= 2)
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            int routes = 2 * (sum - 2) + 1;
            if (playerContext.Player.TradeShipsLimit > routes)
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }            

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
