using EmptyKeys.Strategy.Environment;
using EmptyKeys.Strategy.Units;
using EmptyKeys.Strategy.Units.Tasks;

namespace EmptyKeys.Strategy.AI.Components.ActionsUnit
{
    /// <summary>
    /// Implements unit enter dock action for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class UnitEnterDockAction : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitEnterDockAction"/> class.
        /// </summary>
        public UnitEnterDockAction()
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
            if (unit == null || !unit.CanDock || unit.Environment is Galaxy)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            IDockableUnit dockUnit = unit.Target as IDockableUnit;
            if (dockUnit == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            EnterDockTask task = new EnterDockTask(unit, dockUnit);
            task.Execute();

            if (task.IsTaskFinished && unit.IsInDock)
            {                
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }
            
            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }        
    }
}
