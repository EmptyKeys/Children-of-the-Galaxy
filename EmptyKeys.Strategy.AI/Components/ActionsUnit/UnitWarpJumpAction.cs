using EmptyKeys.Strategy.Units;
using EmptyKeys.Strategy.Units.Tasks;

namespace EmptyKeys.Strategy.AI.Components.Actions
{
    /// <summary>
    /// Implements unit warp jump action for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class UnitWarpJumpAction : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitWarpJumpAction"/> class.
        /// </summary>
        public UnitWarpJumpAction()
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
            if (unit == null || !unit.CanWarp)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }            

            WarpJumpTask task = new WarpJumpTask(unit, unitContext.Unit.Owner.GameSession.Galaxy);
            // TODO: think about this issue 
            while (!task.IsTaskFinished)
            {
                task.Execute();                
            }

            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
