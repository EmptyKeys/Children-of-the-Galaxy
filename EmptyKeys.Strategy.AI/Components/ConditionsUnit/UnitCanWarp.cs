using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.ConditionsUnit
{
    /// <summary>
    /// Implements unit condition for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class UnitCanWarp : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitCanWarp"/> class.
        /// </summary>
        public UnitCanWarp()
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
            if (unit == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            if (unit.CanWarp && unit.Environment != null)
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
