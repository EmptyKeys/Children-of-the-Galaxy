using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Environment;

namespace EmptyKeys.Strategy.AI.Components.ActionsUnit
{
    /// <summary>
    /// Implements unit action for behavior. This action looks for not scanned anomaly.
    /// The result is stored in EnvironmentTarget of UnitBehaviorContext.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class FindUnscannedAnomaly : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindUnscannedPlanet"/> class.
        /// </summary>
        public FindUnscannedAnomaly()
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

            EmptySpace space = unitContext.Unit.Environment as EmptySpace;
            if (space == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            unitContext.EnvironmentTarget = null;

            foreach (var body in space.EnvironmentMap.Values)
            {
                Anomaly anomaly = body as Anomaly;
                if (anomaly == null)
                {
                    continue;
                }

                unitContext.EnvironmentTarget = body;
                break;
            }

            if (unitContext.EnvironmentTarget != null)
            {
                context.AddLogMessage("Anomaly found.");
                returnCode = BehaviorReturnCode.Success;                
            }

            return returnCode;
        }
    }
}
