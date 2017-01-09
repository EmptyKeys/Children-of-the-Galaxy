using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.Actions
{
    /// <summary>
    /// Implements unit action for behavior. This action finds path of the unit to strike group leader.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class FindPathToStrikeGroupLeader : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindPathToStrikeGroupLeader"/> class.
        /// </summary>
        public FindPathToStrikeGroupLeader()
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
            if (unit == null || unit.StrikeGroup == null || unit.StrikeGroup.GroupLeader == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }            
            
            short q = unit.StrikeGroup.GroupLeader.Q;
            short r = unit.StrikeGroup.GroupLeader.R;
            unit.CalculatePath(unit.Environment, q, r);
            if (unit.SelectedPath != null)
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
