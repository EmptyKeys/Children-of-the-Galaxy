using System.Xml.Serialization;
using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Environment;

namespace EmptyKeys.Strategy.AI.Components.ActionsPlayer
{
    /// <summary>
    /// Implements player action for behavior. This action finds best planet to colonize and based on that it finds best planet to build colony ship. Calculations are using utility values.
    /// The result is stored in BehaviorTarget of PlayerBehaviorContext.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class FindPlanetForBuildColonyShip : BehaviorComponentBase
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
        /// Initializes a new instance of the <see cref="FindPlanetForBuildColonyShip"/> class.
        /// </summary>
        public FindPlanetForBuildColonyShip()
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

            float maxPlanetUtility = float.MinValue;
            Planet planetToColonize = null;
            foreach (var galaxyBodyKey in player.ExploredEnvironments)
            {
                HexElement elem = null;
                if (!player.GameSession.Galaxy.EnvironmentMap.TryGetValue(galaxyBodyKey, out elem))
                {
                    continue;
                }

                StarSystem system = elem as StarSystem;
                if (system == null || system.NumberOfPlanets == 0)
                {
                    continue;
                }                

                foreach (var planet in system.Planets)
                {                    
                    if (planet == null || (planet != null && planet.Owner != null) ||
                        !player.CanColonizePlanetType(planet.PlanetType))
                    {
                        continue;
                    }                    

                    int planetScanned = player.ScannedStarSystemBodies.Contains(planet.GlobalKey) ? 1 : 0;
                    float planetPreference = player.Race.PlanetTypePreferences[(int)planet.PlanetType] / 100f;
                    float planetUtility = (planet.AvailResources * planetPreference) / planet.Mass + planet.AvailEnergy / planet.Mass + (planet.RareResources.Count * planetScanned) + planet.MaxSlotsCount;
                    if (maxPlanetUtility > planetUtility)
                    {
                        continue;
                    }

                    maxPlanetUtility = planetUtility;
                    planetToColonize = planet;
                }
            }

            if (planetToColonize == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            float maxUtility = float.MinValue;            
            Planet bestPlanet = null;
            foreach (var body in player.StarSystemBodies)
            {
                Planet planet = body as Planet;
                if (planet == null)
                {
                    continue;
                }

                float distance = HexMap.Distance(planetToColonize, planet);
                if (planetToColonize.Environment != planet.Environment)
                {                    
                    distance = HexMap.Distance(planetToColonize.Environment, planet.Environment) * DistanceSystemUtilityCoefficient;
                }

                float utility = planet.AvailProduction + planet.Population - distance;
                if (maxUtility > utility)
                {
                    continue;
                }

                maxUtility = utility;
                bestPlanet = planet;
            }

            if (bestPlanet != null)
            {
                playerContext.BehaviorTarget = bestPlanet;
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
