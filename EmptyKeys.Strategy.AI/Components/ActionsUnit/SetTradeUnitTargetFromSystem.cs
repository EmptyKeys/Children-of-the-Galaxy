using EmptyKeys.Strategy.Trade;

namespace EmptyKeys.Strategy.AI.Components.Actions
{
    /// <summary>
    /// Implements unit action for behavior. This action swaps system of trade route with planet.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class SetTradeUnitTargetFromSystem : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetTradeUnitTargetFromSystem"/> class.
        /// </summary>
        public SetTradeUnitTargetFromSystem()
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

            if (unitContext.EnvironmentTarget == unit.TradeRoute.From.Environment)
            {
                unitContext.EnvironmentTarget = unit.TradeRoute.From;
            }
            else if (unitContext.EnvironmentTarget == unit.TradeRoute.To.Environment)
            {
                unitContext.EnvironmentTarget = unit.TradeRoute.To;
            }
            else
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
