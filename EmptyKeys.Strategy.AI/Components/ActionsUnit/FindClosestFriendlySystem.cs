using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Environment;

namespace EmptyKeys.Strategy.AI.Components.ActionsUnit
{
    /// <summary>
    /// Implements unit action for behavior. This action finds the closest friendly star system.
    /// The result is stored in EnvironmentTarget of UnitBehaviorContext.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class FindClosestFriendlySystem : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindClosestFriendlySystem"/> class.
        /// </summary>
        public FindClosestFriendlySystem()
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

            foreach (var body in unitContext.Unit.Owner.StarSystemBodies)
            {
                Planet planet = body as Planet;
                if (planet == null)
                {
                    continue;
                }

                int distance = HexMap.Distance(unitContext.Unit, planet.Environment);
                if (minDistance > distance)
                {
                    minDistance = distance;
                    closestEnvi = planet.Environment;
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
