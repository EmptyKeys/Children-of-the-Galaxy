using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Environment;
using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.Conditions
{
    /// <summary>
    /// Implements unit condition for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class UnitCanOrbit : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitCanOrbit"/> class.
        /// </summary>
        public UnitCanOrbit()
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
            if (unit == null || unit.Environment == null || !unit.CanMove || unit.IsOnOrbit || unit.IsInDock)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }            

            HexElement element;
            if (!unit.Environment.UnitsMap.TryGetValue(unit.HexMapKey, out element))
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            if (!unit.Environment.EnvironmentMap.TryGetValue(unit.HexMapKey, out element))
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            if (element is IOrbitable)
            {
                IOrbitable bodyWithOrbit = (IOrbitable)element;
                if (bodyWithOrbit.CanEnterMoreUnits(unit.Owner))
                {
                    returnCode = BehaviorReturnCode.Success;
                    return returnCode;
                }
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
