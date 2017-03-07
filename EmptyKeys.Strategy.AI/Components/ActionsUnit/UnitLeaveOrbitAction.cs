using EmptyKeys.Strategy.Environment;
using EmptyKeys.Strategy.Units;
using EmptyKeys.Strategy.Units.Tasks;

namespace EmptyKeys.Strategy.AI.Components.ActionsUnit
{
    /// <summary>
    /// Implements unit leave orbit action for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class UnitLeaveOrbitAction : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitLeaveOrbitAction"/> class.
        /// </summary>
        public UnitLeaveOrbitAction()
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
            if (unit == null || unit.IsDead)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            Planet planet = unitContext.Unit.GetOrbitingPlanet();
            if (planet == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            LeaveOrbitTask task = new LeaveOrbitTask(unit, planet);
            task.Execute();

            if (task.IsTaskFinished)
            {                
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }
            
            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
