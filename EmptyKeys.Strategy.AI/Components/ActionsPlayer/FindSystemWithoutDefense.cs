using System.Collections.Generic;
using System.Linq;
using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Environment;
using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.ActionsPlayer
{
    /// <summary>
    /// Implements player action for behavior. This action finds owned star system without any defense.
    /// The result is stored in EnvironmentTarget of PlayerBehaviorContext.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class FindSystemWithoutDefense : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindSystemWithoutDefense"/> class.
        /// </summary>
        public FindSystemWithoutDefense()
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
            PlayerBehaviorContext playerContext = context as PlayerBehaviorContext;
            if (playerContext == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            Player player = playerContext.Player;
            foreach (var body in player.StarSystemBodies)
            {
                Planet planet = body as Planet;
                if (planet == null)
                {
                    continue;
                }

                BaseEnvironment envi = planet.Environment;
                List<BaseUnit> enviUnits = player.Units.Where(u => u.Environment == envi).ToList();
                HexElement defenseUnit = enviUnits.FirstOrDefault(u => u is DefenseTowerUnit || u is Station);
                if (defenseUnit == null)
                {
                    BaseUnit existingUnit = enviUnits.FirstOrDefault(u => u is Builder && u.Behavior == null);
                    playerContext.BehaviorTarget = existingUnit;
                    if (existingUnit != null)
                    {
                        playerContext.EnvironmentTarget = planet.Environment;
                        returnCode = BehaviorReturnCode.Success;
                        return returnCode;
                    }
                }
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
