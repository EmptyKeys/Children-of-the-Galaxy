using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Environment;

namespace EmptyKeys.Strategy.AI.Components.ActionsUnit
{
    /// <summary>
    /// Implements unit action for behavior. This action finds planet, which is under siege, and unit can enter orbit around it.
    /// The result is stored in EnvironmentTarget of UnitBehaviorContext.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class FindPlanetUnderSiege : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindPlanetUnderSiege"/> class.
        /// </summary>
        public FindPlanetUnderSiege()
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

            Player player = unitContext.Unit.Owner;
            foreach (var body in player.StarSystemBodies)
            {
                Planet planet = body as Planet;
                if (planet == null || planet.Environment != envi || !planet.IsUnderSiege || !planet.CanEnterMoreUnits(player))
                {
                    continue;
                }

                unitContext.EnvironmentTarget = planet;
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
