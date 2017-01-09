using System.Linq;
using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Environment;
using EmptyKeys.Strategy.Trade;
using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.Actions
{
    /// <summary>
    /// Implements unit action for behavior. This action finds the closest trade system with available trade route.
    /// The result is stored in EnvironmentTarget of UnitBehaviorContext.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class FindClosestTradeSystem : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindClosestTradeSystem"/> class.
        /// </summary>
        public FindClosestTradeSystem()
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

            BaseUnit unit = unitContext.Unit as BaseUnit;
            if (unit == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            Player player = unitContext.Unit.Owner;
            BaseEnvironment tradeSystem = null;
            int minDistance = int.MaxValue;
            foreach (var elem in player.StarSystemBodies)
            {
                ITradePoint tradePoint = elem as ITradePoint;
                if (tradePoint == null || tradePoint.Environment == unit.Environment)
                {
                    continue;
                }

                int activeTradeRoutesCount = tradePoint.Owner.TradeRoutes.Count(tr => tr.From == elem);
                if ((activeTradeRoutesCount - tradePoint.TradeRoutesCount) == 0)
                {
                    continue;
                }

                int distance = HexMap.Distance(unit.Environment, elem);
                if (minDistance > distance)
                {
                    minDistance = distance;
                    tradeSystem = tradePoint.Environment;
                }
            }

            if (tradeSystem != null)
            {
                unitContext.EnvironmentTarget = tradeSystem;
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
