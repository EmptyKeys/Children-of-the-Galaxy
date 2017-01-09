using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.Conditions
{
    /// <summary>
    /// Implements unit condition for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class StrikeGroupIsDockUnitMember : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StrikeGroupIsDockUnitMember"/> class.
        /// </summary>
        public StrikeGroupIsDockUnitMember()
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
            if (unitContext == null || unitContext.Unit == null || unitContext.Unit.StrikeGroup == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }
            
            MoveableUnit unit = unitContext.Unit as MoveableUnit;
            if (!unit.IsInDock)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            MoveableUnit dockUnit = unit.GetDockUnit() as MoveableUnit;
            if (dockUnit != null && dockUnit.StrikeGroup == unit.StrikeGroup)
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
