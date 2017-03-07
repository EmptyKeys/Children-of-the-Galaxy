using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.ConditionsUnit
{
    /// <summary>
    /// Implements unit condition for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class UnitIsTargetInSystem : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitIsTargetInSystem"/> class.
        /// </summary>
        public UnitIsTargetInSystem()
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

            StaticUnit unit = unitContext.Unit as StaticUnit;
            if (unit == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            StaticUnit target = unit.Target as StaticUnit;
            if (target == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            if (unit.Environment == target.Environment)
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
