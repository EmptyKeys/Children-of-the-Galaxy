using System.Diagnostics;
using EmptyKeys.Strategy.Environment;

namespace EmptyKeys.Strategy.AI.Components.ActionsUnit
{
    /// <summary>
    /// Implements unit action for behavior. This action sets TargetPlanet to EnvironmentTarget of orbiting planet.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class SetTargetPlanet : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetTargetPlanet"/> class.
        /// </summary>
        public SetTargetPlanet()
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

            Planet orbitingPlanet = unitContext.Unit.GetOrbitingPlanet();
            if (orbitingPlanet == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            if (unitContext.TargetPlanet == null)
            {
                unitContext.TargetPlanet = orbitingPlanet.BehaviorContext.EnvironmentTarget as Planet;
                orbitingPlanet.BehaviorContext.EnvironmentTarget = null;
            }

            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
