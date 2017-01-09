using System.Collections.Generic;
using System.Linq;
using EmptyKeys.Strategy.Environment;
using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.ConditionsPlanet
{
    /// <summary>
    /// Implements planet condition for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class PlanetAnyBuilderUnitInSystem : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlanetAnyBuilderUnitInSystem"/> class.
        /// </summary>
        public PlanetAnyBuilderUnitInSystem() : base()
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

            List<BaseUnit> units = planetContext.Planet.Owner.Units.Where(u => u.Environment == planetContext.Planet.Environment).ToList();
            foreach (var unit in units)
            {
                Builder builder = unit as Builder;
                if (builder == null)
                {
                    continue;
                }

                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            List<StarSystemBody> envBodies = planetContext.Planet.Owner.StarSystemBodies.Where(u => u.Environment == planetContext.Planet.Environment).ToList();
            foreach (var body in envBodies)
            {
                Planet planet = body as Planet;
                if (planet == null)
                {
                    continue;
                }

                var builder = planet.FactoryQueue.FirstOrDefault(i => i.Item.FactoryTypeName.Equals("Builder"));
                if (builder == null)
                {
                    continue;
                }

                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
