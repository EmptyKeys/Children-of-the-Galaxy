using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.ConditionsUnit
{
    /// <summary>
    /// Implements unit condition for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class UnitCanExtract : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitCanExtract"/> class.
        /// </summary>
        public UnitCanExtract()
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

            ResourcesExtractor unit = unitContext.Unit as ResourcesExtractor;
            if (unit == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            if (unit.IsExtracting || unit.IsSleeping)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
