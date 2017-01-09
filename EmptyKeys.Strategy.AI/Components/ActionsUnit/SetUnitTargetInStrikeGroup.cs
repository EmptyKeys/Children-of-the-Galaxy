using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.Actions
{
    /// <summary>
    /// Implements unit action for behavior. This action sets unit's Target to strike group leader Target.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class SetUnitTargetInStrikeGroup : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetUnitTargetInStrikeGroup"/> class.
        /// </summary>
        public SetUnitTargetInStrikeGroup()
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

            StaticUnit staticUnit = unitContext.Unit as StaticUnit;
            StaticUnit leader = unitContext.Unit.StrikeGroup.GroupLeader as StaticUnit;
            if (staticUnit == null || leader == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            staticUnit.Target = leader.Target;

            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
