using System.Xml.Serialization;
using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Environment;

namespace EmptyKeys.Strategy.AI.Components.ActionsPlayer
{
    /// <summary>
    /// Implements player action for behavior. This action finds the closest enemy system.
    /// Enemy system is found based on utility value calculated from influence and distance.
    /// The result is stored in EnvironmentTarget of PlayerBehaviorContext.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class FindClosestEnemySystem : BehaviorComponentBase
    {
        /// <summary>
        /// Gets or sets the distance system utility coefficient.
        /// </summary>
        /// <value>
        /// The distance system utility coefficient.
        /// </value>
        [XmlAttribute]
        public float DistanceSystemUtilityCoefficient { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FindClosestEnemySystem"/> class.
        /// </summary>
        public FindClosestEnemySystem()
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
            BaseEnvironment enemySystem = null;
            float maxUtility = float.MinValue;
            foreach (var elem in player.GameSession.Galaxy.EnvironmentMap.Values)
            {
                BaseEnvironment envi = elem as BaseEnvironment;
                if (envi == null || !player.ExploredEnvironments.Contains(envi.HexMapKey))
                {
                    continue;
                }

                int distance = HexMap.Distance(player.HomeStarSystem, envi);
                foreach (var item in envi.PlayersInfluence)
                {
                    if (item.Key == playerContext.Player)
                    {
                        continue;
                    }

                    if (!(player.IsAtWar(item.Key) || item.Key.IsAtWar(player)))
                    {
                        continue;
                    }
                    
                    float utility = item.Value - distance * DistanceSystemUtilityCoefficient;
                    if (maxUtility > utility)
                    {
                        continue;
                    }

                    maxUtility = utility;
                    enemySystem = envi;
                }
            }

            if (enemySystem != null)
            {
                playerContext.EnvironmentTarget = enemySystem;
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
