using System.Diagnostics;
using System.Xml.Serialization;
using EmptyKeys.Strategy.Environment;
using EmptyKeys.Strategy.Environment.PlanetUpgrades;

namespace EmptyKeys.Strategy.AI.Components.ActionsPlayer
{
    /// <summary>
    /// Implements player action for behavior. This action finds Planet with minimum value in specific attribute.
    /// The result is stored in BehaviorTarget of PlayerBehaviorContext.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class FindPlanetWithMinValue : BehaviorComponentBase
    {
        /// <summary>
        /// Gets or sets the type of the planet value.
        /// </summary>
        /// <value>
        /// The type of the planet value.
        /// </value>
        [XmlAttribute]
        public UpgradeModifierType PlanetValueType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FindPlanetWithMinValue"/> class.
        /// </summary>
        public FindPlanetWithMinValue()
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

            float minValue = float.MaxValue;
            Planet bestPlanet = null;
            foreach (var body in playerContext.Player.StarSystemBodies)
            {
                Planet planet = body as Planet;
                if (planet == null)
                {
                    continue;
                }

                if (planet.IsUnderSiege)
                {
                    continue;
                }

                float planetValue = GetPlanetValue(planet);
                if (minValue < planetValue)
                {
                    continue;
                }

                minValue = planetValue;
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

        private float GetPlanetValue(Planet planet)
        {
            float planetValue = 0;
            switch (PlanetValueType)
            {                
                case UpgradeModifierType.Energy:
                    planetValue = planet.AvailEnergy;
                    break;
                case UpgradeModifierType.Population:
                    planetValue = planet.Population;
                    break;
                case UpgradeModifierType.Resources:
                    planetValue = planet.AvailResources;
                    break;
                case UpgradeModifierType.Production:
                    planetValue = planet.AvailProduction;
                    break;
                case UpgradeModifierType.Research:
                    planetValue = planet.AvailResearch;
                    break;
                case UpgradeModifierType.Intelligence:
                    planetValue = planet.Intelligence;
                    break;
                case UpgradeModifierType.TradeDistanceBonus:
                    planetValue = planet.TradeDistanceBonus;
                    break;
                case UpgradeModifierType.TradeRoutesCount:
                    planetValue = planet.TradeRoutesCount;
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }

            return planetValue;
        }
    }
}
