using EmptyKeys.Strategy.Units;
using EmptyKeys.Strategy.Units.Tasks;

namespace EmptyKeys.Strategy.AI.Components.ActionsUnit
{
    /// <summary>
    /// Implements unit extract action for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class UnitExtractAction : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitExtractAction"/> class.
        /// </summary>
        public UnitExtractAction()
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

            ExtractTask task = new ExtractTask(unit);
            task.Execute();            
            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
