using System.Linq;
using System.Xml.Serialization;
using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Environment;
using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.Actions
{
    /// <summary>
    /// Implements unit action for behavior. This action finds Planet for colonization.
    /// The result is stored in EnvironmentTarget of UnitBehaviorContext.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class FindPlanetToColonize : BehaviorComponentBase
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
        /// Initializes a new instance of the <see cref="FindPlanetToColonize"/> class.
        /// </summary>
        public FindPlanetToColonize()
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

            float maxPlanetUtility = float.MinValue;
            Planet selectedPlanet = null;
            var player = unitContext.Unit.Owner;
            foreach (var galaxyBodyKey in player.ExploredEnvironments)
            {
                HexElement elem = null;
                if (!player.GameSession.Galaxy.EnvironmentMap.TryGetValue(galaxyBodyKey, out elem))
                {
                    continue;
                }

                // TODO: rogue planets colonization
                StarSystem system = elem as StarSystem;
                if (system == null || system.NumberOfPlanets == 0 || !system.HasHighestInfluence(player))
                {
                    continue;
                }                

                int systemDistance = HexMap.Distance(envi, system);
                if (envi is Galaxy)
                {
                    systemDistance = HexMap.Distance(unitContext.Unit, system);
                }

                foreach (var planet in system.Planets)
                {                    
                    if (planet == null || planet.Owner != null || !player.CanColonizePlanetType(planet.PlanetType))
                    {
                        continue;
                    }
                                        
                    var existingColonyShip = player.Units.FirstOrDefault(u => u.Item.UnitConfig.Actions.HasFlag(UnitActions.Colonize) &&
                                                                              u != unitContext.Unit && 
                                                                              u.BehaviorContext.EnvironmentTarget == planet);
                    if (existingColonyShip != null)
                    {
                        continue;
                    }

                    int planetDistance = 0;
                    if (envi == system)
                    {
                        planetDistance = HexMap.Distance(unitContext.Unit, planet);
                    }

                    bool planetScanned = player.ScannedStarSystemBodies.Contains(planet.GlobalKey);
                    float rareResourcesValue = 0;
                    if (planetScanned)
                    {
                        foreach (var rareResource in planet.RareResources)
                        {
                            rareResourcesValue += rareResource.Quantity;
                        }

                        rareResourcesValue = rareResourcesValue / 1000;
                    }

                    float planetPreference = player.Race.PlanetTypePreferences[(int)planet.PlanetType] / 100f;
                    float planetUtility = (planet.AvailResources * planetPreference) / planet.Mass + planet.AvailEnergy / planet.Mass + rareResourcesValue 
                                           - systemDistance * DistanceSystemUtilityCoefficient - planetDistance + planet.MaxSlotsCount;

                    var isHomePlanet = player.GameSession.Players.FirstOrDefault(p => p.HomePlanet == planet);
                    if (isHomePlanet != null)
                    {
                        selectedPlanet = planet;
                        break;
                    }

                    if (maxPlanetUtility > planetUtility)
                    {
                        continue;
                    }

                    maxPlanetUtility = planetUtility;
                    selectedPlanet = planet;
                }
            }

            if (selectedPlanet != null)
            {
                unitContext.EnvironmentTarget = selectedPlanet;
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
