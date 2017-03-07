using EmptyKeys.Strategy.Environment;
using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.ActionsUnit
{
    /// <summary>
    /// Implements unit action for behavior. This action finds the strongest friendly unit in the system.
    /// The result is stored in Target of Unit.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class FindStrongestFriendlyUnit : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindStrongestFriendlyUnit"/> class.
        /// </summary>
        public FindStrongestFriendlyUnit()
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

            BaseEnvironment envi = unit.Environment;
            if (envi == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            BaseUnit strongestFriendlyUnit = null;
            float maxInfluence = float.MinValue;

            foreach (var elem in envi.UnitsMap.Values)
            {
                BaseUnit target = elem as BaseUnit;
                if (target == null || target.Owner != unitContext.Unit.Owner)
                {
                    continue;
                }

                if (target.Influence > maxInfluence)
                {
                    maxInfluence = target.Influence;
                    strongestFriendlyUnit = target;
                }
            }

            unit.Target = strongestFriendlyUnit;
            if (strongestFriendlyUnit != null)
            {
                context.AddLogMessage("Friendly found - " + strongestFriendlyUnit.Name);
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
