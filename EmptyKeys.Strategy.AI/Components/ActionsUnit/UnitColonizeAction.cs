using EmptyKeys.Strategy.Environment;
using EmptyKeys.Strategy.Units;
using EmptyKeys.Strategy.Units.Tasks;

namespace EmptyKeys.Strategy.AI.Components.ActionsUnit
{
    /// <summary>
    /// Implements unit colonize action for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class UnitColonizeAction : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitColonizeAction"/> class.
        /// </summary>
        public UnitColonizeAction()
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

            ColonyShip unit = unitContext.Unit as ColonyShip;
            if (unit == null || !unit.IsOnOrbit)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            Planet planet = unitContext.EnvironmentTarget as Planet;
            if (planet == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            ColonizeTask task = new ColonizeTask(unit, planet);

            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
