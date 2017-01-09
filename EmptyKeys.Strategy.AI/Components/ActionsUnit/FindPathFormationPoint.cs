using System.Linq;
using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.Actions
{
    /// <summary>
    /// Implements unit action for behavior. This action finds path to formation point of the unit in the strike group.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class FindPathFormationPoint : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindPathFormationPoint"/> class.
        /// </summary>
        public FindPathFormationPoint()
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
            if (unit == null || unit.StrikeGroup == null || unit.StrikeGroup.FormationPoints == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            var formationPoint = unit.StrikeGroup.FormationPoints.FirstOrDefault(p => p.UnitGlobalKey == unit.GlobalKey);
            if (formationPoint == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }
            
            short q = (short)(unit.StrikeGroup.GroupLeader.Q + formationPoint.LocalQ);
            short r = (short)(unit.StrikeGroup.GroupLeader.R + formationPoint.LocalR);
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
