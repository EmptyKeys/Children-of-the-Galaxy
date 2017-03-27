using System.Diagnostics;
using EmptyKeys.Strategy.Environment;

namespace EmptyKeys.Strategy.AI.Components.ActionsUnit
{
    /// <summary>
    /// Implements unit action for behavior. This action sets EnvironmentTarget to TargetPlanet.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class SetPlanetAsEnvTarget : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetPlanetAsEnvTarget"/> class.
        /// </summary>
        public SetPlanetAsEnvTarget()
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
            
            unitContext.EnvironmentTarget = unitContext.TargetPlanet;

            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
