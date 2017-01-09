using System.Linq;
using EmptyKeys.Strategy.Environment.Factory;

namespace EmptyKeys.Strategy.AI.Components.ConditionsPlanet
{
    /// <summary>
    /// Implements planet condition for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class PlanetIsBuilding : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlanetIsBuilding"/> class.
        /// </summary>
        public PlanetIsBuilding()
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
            PlanetBehaviorContext planetContext = context as PlanetBehaviorContext;
            if (planetContext == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            FactoryTask task = planetContext.Planet.FactoryQueue.FirstOrDefault(t => t is BuildFactoryTask);
            if (task != null)
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
