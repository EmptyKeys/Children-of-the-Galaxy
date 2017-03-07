using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.ActionsUnit
{
    /// <summary>
    /// Implements action, which resets Target of the unit.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class UnitResetTarget : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitResetTarget"/> class.
        /// </summary>
        public UnitResetTarget()
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

            unit.Target = null;

            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
