using System.Xml.Serialization;
using EmptyKeys.Strategy.Trade;
using EmptyKeys.Strategy.Units.Tasks;

namespace EmptyKeys.Strategy.AI.Components.ActionsPlayer
{
    /// <summary>
    /// Implements player action for behavior. This action sets Trade Route of trade unit (BehaviorTarget).
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class PlayerSetTradeRoute : BehaviorComponentBase
    {
        /// <summary>
        /// Gets or sets a value indicating whether [include domestic].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [include domestic]; otherwise, <c>false</c>.
        /// </value>
        [XmlAttribute]
        public bool IncludeDomestic { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [include global].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [include global]; otherwise, <c>false</c>.
        /// </value>
        [XmlAttribute]
        public bool IncludeGlobal { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [automatic renew].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [automatic renew]; otherwise, <c>false</c>.
        /// </value>
        [XmlAttribute]
        public bool AutoRenew { get; set; }

        /// <summary>
        /// Gets or sets the trade ballance.
        /// </summary>
        /// <value>
        /// The trade ballance.
        /// </value>
        [XmlAttribute]
        public float TradeBallance { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerSetTradeRoute"/> class.
        /// </summary>
        public PlayerSetTradeRoute()
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

            ITradeUnit tradeUnit = playerContext.BehaviorTarget as ITradeUnit;
            if (tradeUnit == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            var availRoutes = tradeUnit.Owner.GetAvailableTradeRoutes(tradeUnit, IncludeDomestic, IncludeGlobal);
            if (availRoutes == null || availRoutes.Count == 0)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            if (tradeUnit.HasAssignedTradeRoute)
            {
                tradeUnit.CancelTrade();
            }

            // TODO: select best trade route here
            BaseTradeRoute route = availRoutes[0];
            route.AutoRenew = AutoRenew;
            route.Balance = TradeBallance;
            route.UpdateBalance();

            TradeTask task = new TradeTask(tradeUnit, route);
            task.Execute();

            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
