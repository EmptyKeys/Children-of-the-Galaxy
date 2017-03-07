using System;
using System.Diagnostics;
using System.Linq;
using System.Xml.Serialization;
using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Environment;
using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.Considerations
{
    /// <summary>
    /// Implements Consideration value for Utility based AI. This consideration value represents colonization ratio of context Player.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.Considerations.BaseConsiderationValue" />
    public class ColonizationRatioValue : BaseConsiderationValue
    {
        private float planetTypesCount;

        /// <summary>
        /// Gets or sets the ratio.
        /// </summary>
        /// <value>
        /// The ratio.
        /// </value>
        [XmlAttribute]
        public float Ratio { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColonizationRatioValue"/> class.
        /// </summary>
        public ColonizationRatioValue()
            : base()
        {
            planetTypesCount = Enum.GetValues(typeof(PlanetType)).Length;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override float GetValue(IBehaviorContext context)
        {
            context.AddLogMessage("ColonizationRatioValue");
            PlayerBehaviorContext playerContext = context as PlayerBehaviorContext;
            if (playerContext == null)
            {
                Debug.Assert(false, "Wrong Behavior Context");
                return 0;
            }

            Player player = playerContext.Player;
            float colonyShipCount = player.Units.Count(u => u is ColonyShip);

            foreach (var body in player.StarSystemBodies)
            {
                Planet planet = body as Planet;
                if (planet == null || planet.FactoryQueue.Count == 0)
                {
                    continue;
                }

                var colonyShip = planet.FactoryQueue.FirstOrDefault(f => f.Item.UnitConfig.Actions.HasFlag(UnitActions.Colonize));
                if (colonyShip != null)
                {
                    colonyShipCount++;
                }
            }

            float planetTypesRatio = player.ColonizablePlanetTypes.Count / planetTypesCount;
            int scannedColonizablePlanets = player.ScannedPlanets.Count;
            if (planetTypesRatio != 1)
            {
                scannedColonizablePlanets = player.ScannedPlanets.Count(p => player.CanColonizePlanetType(p.PlanetType));
            }

            float value = (scannedColonizablePlanets - player.TotalPlanets - (colonyShipCount * Ratio)) / (player.GameSession.Galaxy.TotalPlanets * planetTypesRatio);

            return value;
        }
    }
}
