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
            float colonyShips = player.Units.Count(u => u is ColonyShip);
            float planetTypesRatio = player.ColonizablePlanetTypes.Count / planetTypesCount;
            float scannedPlanets = player.TotalScannedPlanets;// * planetTypesRatio;
            float value = (scannedPlanets - player.TotalPlanets - (colonyShips * Ratio)) / (player.GameSession.Galaxy.TotalPlanets * planetTypesRatio);

            return value;
        }
    }
}
