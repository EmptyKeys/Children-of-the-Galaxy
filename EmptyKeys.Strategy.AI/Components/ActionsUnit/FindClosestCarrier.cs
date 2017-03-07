using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Environment;
using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.ActionsUnit
{
    /// <summary>
    /// Implements unit action for behavior. This action finds the closest Carrier unit with empty slots.
    /// The result if stored in Target of Unit.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class FindClosestCarrier : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindClosestCarrier"/> class.
        /// </summary>
        public FindClosestCarrier()
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

            BaseEnvironment envi = unitContext.Unit.Environment;
            if (envi is Galaxy || envi == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            MoveableUnit unit = unitContext.Unit as MoveableUnit;
            if (unit.IsInDock)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }            

            Carrier closestCarrier = null;
            int minDistance = int.MaxValue;
            foreach (var elem in envi.UnitsMap.Values)
            {
                Carrier carrier = elem as Carrier;
                if (carrier == null)
                {
                    continue;
                }

                if (carrier.Dock.CurrentDockSize + unit.DockSize > carrier.Dock.MaxDockSize)
                {
                    continue;
                }

                int distance = HexMap.Distance(unit, carrier);
                if (distance > minDistance)
                {
                    continue;
                }

                closestCarrier = carrier;
                minDistance = distance;
            }

            unit.Target = closestCarrier;
            if (closestCarrier != null)
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
