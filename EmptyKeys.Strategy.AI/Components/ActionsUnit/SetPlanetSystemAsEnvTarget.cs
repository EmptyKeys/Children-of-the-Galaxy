using System.Diagnostics;
using EmptyKeys.Strategy.Environment;

namespace EmptyKeys.Strategy.AI.Components.ActionsUnit
{
    /// <summary>
    /// Implements unit action for behavior. This action gets Planet from EnvironmentTarget and sets EnvironmentTarget to planet's Environment.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class SetPlanetSystemAsEnvTarget : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetPlanetSystemAsEnvTarget"/> class.
        /// </summary>
        public SetPlanetSystemAsEnvTarget()
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

            Planet planet = unitContext.EnvironmentTarget as Planet;
            if (planet == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            Debug.Assert(planet.Environment != null, "Planet Environment is null!");
            unitContext.EnvironmentTarget = planet.Environment;

            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
