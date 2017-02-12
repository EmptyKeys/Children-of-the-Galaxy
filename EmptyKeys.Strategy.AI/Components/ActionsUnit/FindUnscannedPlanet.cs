using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Environment;

namespace EmptyKeys.Strategy.AI.Components.Actions
{
    /// <summary>
    /// Implements unit action for behavior. This action finds not scanned, colonizable and the nearest Planet.
    /// The result is stored in EnvironmentTarget of UnitBehaviorContext.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class FindUnscannedPlanet : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindUnscannedPlanet"/> class.
        /// </summary>
        public FindUnscannedPlanet()
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

            StarSystem system = unitContext.Unit.Environment as StarSystem;
            if (system == null || system.Planets == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            unitContext.EnvironmentTarget = null;
            int minDistance = int.MaxValue;

            foreach (var planet in system.Planets)
            {
                if (unitContext.Unit.Owner.ScannedStarSystemBodies.Contains(planet.GlobalKey))
                {
                    continue;
                }

                if (system.UnitsMap.ContainsKey(planet.HexMapKey))
                {
                    continue;
                }

                if (planet.Owner != null)
                {
                    continue;
                }

                /*
                if (!unitContext.Unit.Owner.ColonizablePlanetTypes.Contains(planet.PlanetType))
                {
                    continue;
                }
                */

                int distance = HexMap.Distance(unitContext.Unit, planet.Environment);
                if (minDistance > distance)
                {
                    minDistance = distance;
                    unitContext.EnvironmentTarget = planet;
                }                                
            }

            if (unitContext.EnvironmentTarget != null)
            {
                context.AddLogMessage("Planet found - " + ((Planet)unitContext.EnvironmentTarget).Name);
                returnCode = BehaviorReturnCode.Success;                
            }

            return returnCode;
        }
    }
}
