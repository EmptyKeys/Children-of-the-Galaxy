using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.ActionsUnit
{
    /// <summary>
    /// Implements unit wake up action for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class UnitWakeUpAction : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitWakeUpAction"/> class.
        /// </summary>
        public UnitWakeUpAction()
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

            if (unit.IsSleeping)
            {
                unit.WakeUpUnit();
            }

            if (!unit.IsSleeping)
            {                
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }
            
            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
