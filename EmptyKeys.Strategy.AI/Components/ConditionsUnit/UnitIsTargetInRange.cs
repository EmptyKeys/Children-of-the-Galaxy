using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.Conditions
{
    /// <summary>
    /// Implements unit condition for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class UnitIsTargetInRange : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitIsTargetInRange"/> class.
        /// </summary>
        public UnitIsTargetInRange()
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

            if (unit.Target == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            int targetDistance = HexMap.Distance(unit.Target, unit);
            if (targetDistance > unit.SensorsEnergy)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
