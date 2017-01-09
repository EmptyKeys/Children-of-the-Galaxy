using EmptyKeys.Strategy.Units.Tasks;

namespace EmptyKeys.Strategy.AI.Components.Actions
{
    /// <summary>
    /// Implements unit alert action for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class UnitAlertAction : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitAlertAction"/> class.
        /// </summary>
        public UnitAlertAction()
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

            if (!unitContext.Unit.CanStayAlert)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            AlertTask task = new AlertTask(unitContext.Unit);
            task.Execute();
            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
