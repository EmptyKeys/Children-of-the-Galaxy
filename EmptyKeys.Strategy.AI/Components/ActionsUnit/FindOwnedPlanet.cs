using EmptyKeys.Strategy.Environment;

namespace EmptyKeys.Strategy.AI.Components.Actions
{
    /// <summary>
    /// Implements unit action for behavior. This action finds owned Planet in the star system.
    /// The result is stored in EnvironmentTarget of UnitBehaviorContext.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class FindOwnedPlanet : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindOwnedPlanet"/> class.
        /// </summary>
        public FindOwnedPlanet()
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

            BaseEnvironment envi = unitContext.Unit.Environment;
            if (envi == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            unitContext.EnvironmentTarget = null;
            foreach (var body in envi.EnvironmentMap.Values)
            {
                Planet planet = body as Planet;
                if (planet == null)
                {
                    continue;
                }                

                if (envi.UnitsMap.ContainsKey(planet.HexMapKey))
                {
                    continue;
                }

                if (planet.Owner != unitContext.Unit.Owner)
                {
                    continue;
                }

                if (!planet.CanEnterMoreUnits(unitContext.Unit.Owner))
                {
                    continue;
                }

                unitContext.EnvironmentTarget = planet;
                context.AddLogMessage("Planet found - " + planet.Name);
                returnCode = BehaviorReturnCode.Success;
                break;
            }

            return returnCode;
        }
    }
}
