using EmptyKeys.Strategy.Units;
using EmptyKeys.Strategy.Units.Tasks;

namespace EmptyKeys.Strategy.AI.Components.ActionsUnit
{
    /// <summary>
    /// Implements unit explore action for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class UnitExploreAction : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitExploreAction"/> class.
        /// </summary>
        public UnitExploreAction()
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
            if (unit == null || !unit.CanExplore)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }            

            ExploreTask exploreTask = new ExploreTask(unit);
            exploreTask.Execute();

            if (exploreTask.IsTaskFinished)
            {
                unit.Environment = exploreTask.ExploredEnvironment;
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }
            
            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
