using EmptyKeys.Strategy.Units;
using EmptyKeys.Strategy.Units.Tasks;

namespace EmptyKeys.Strategy.AI.Components.ActionsUnit
{
    /// <summary>
    /// Implements unit move action for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class UnitMoveAction : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitMoveAction"/> class.
        /// </summary>
        public UnitMoveAction()
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

            MoveableUnit unit = unitContext.Unit as MoveableUnit;
            if (unit == null || !unit.CanMove)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }
            
            MoveTask task = new MoveTask(unit);
            while (!task.IsTurnProcessFinished && !task.IsTaskFinished)
            {
                task.Execute();
            }

            unit.SelectedPath = null;
            if (task.IsTurnProcessFinished)
            {                
                returnCode = BehaviorReturnCode.Running;
            }

            if (task.IsTaskFinished)
            {                
                returnCode = BehaviorReturnCode.Success;
            }

            return returnCode;
        }
    }
}
