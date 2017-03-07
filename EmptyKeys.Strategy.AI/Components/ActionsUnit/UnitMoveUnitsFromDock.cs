using System.Collections.Generic;
using System.Linq;
using EmptyKeys.Strategy.Units;
using EmptyKeys.Strategy.Units.Tasks;

namespace EmptyKeys.Strategy.AI.Components.ActionsUnit
{
    /// <summary>
    /// Implements action to move all units from the Dock for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class UnitMoveUnitsFromDock : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitMoveUnitsFromDock"/> class.
        /// </summary>
        public UnitMoveUnitsFromDock()
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

            Carrier carrier = unitContext.Unit as Carrier;
            if (carrier == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            List<MoveableUnit> units = carrier.Dock.UnitsInDock.ToList();
            foreach (var unit in units)
            {
                LeaveDockTask task = new LeaveDockTask(unit, carrier);
                task.Execute();

                if (!task.IsTaskFinished)
                {
                    returnCode = BehaviorReturnCode.Failure;
                    return returnCode;
                }
            }

            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
