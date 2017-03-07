using EmptyKeys.Strategy.Trade;

namespace EmptyKeys.Strategy.AI.Components.ActionsUnit
{
    /// <summary>
    /// Implements unit action for behavior. This action swaps trade route destination.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class SwapTradeRouteTarget : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SwapTradeRouteTarget"/> class.
        /// </summary>
        public SwapTradeRouteTarget()
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
            UnitBehaviorContext unitContext = context as UnitBehaviorContext;
            if (unitContext == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            ITradeUnit unit = unitContext.Unit as ITradeUnit;
            if (unit == null || !unit.HasAssignedTradeRoute)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            if (unitContext.EnvironmentTarget == unit.TradeRoute.From)
            {
                unitContext.EnvironmentTarget = unit.TradeRoute.To;
            }
            else
            {
                unitContext.EnvironmentTarget = unit.TradeRoute.From;
            }

            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
