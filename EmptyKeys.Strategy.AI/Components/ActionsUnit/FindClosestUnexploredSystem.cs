using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Environment;

namespace EmptyKeys.Strategy.AI.Components.Actions
{
    /// <summary>
    /// Implements unit action for behavior. This action finds the closest unexplored system.
    /// The result is stored in EnvironmentTarget of UnitBehaviorContext.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class FindClosestUnexploredSystem : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindClosestUnexploredSystem"/> class.
        /// </summary>
        public FindClosestUnexploredSystem()
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
            
            BaseEnvironment closestEnvi = null;
            int minDistance = int.MaxValue;
            foreach (var elem in unitContext.Unit.Owner.GameSession.Galaxy.EnvironmentMap.Values)
            {
                BaseEnvironment envi = elem as BaseEnvironment;
                if (envi == null || !(envi is StarSystem))
                {
                    continue;
                }

                if (unitContext.Unit.Owner.ExploredEnvironments.Contains(envi.HexMapKey))
                {
                    continue;
                }

                int distance = HexMap.Distance(unitContext.Unit, envi);
                if (minDistance > distance)
                {
                    minDistance = distance;
                    closestEnvi = envi;
                }
            }

            unitContext.EnvironmentTarget = closestEnvi;
            if (closestEnvi != null)
            {
                context.AddLogMessage("System found - " + closestEnvi.Name);                
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
