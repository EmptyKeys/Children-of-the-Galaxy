using System.Diagnostics;
using System.Linq;
using System.Xml.Serialization;
using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.Considerations
{
    /// <summary>
    /// Implements Consideration Value for Utility based AI. This value represents Exploration ratio.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.Considerations.BaseConsiderationValue" />
    public class ExplorationRatioValue : BaseConsiderationValue
    {
        /// <summary>
        /// Gets or sets the ratio.
        /// </summary>
        /// <value>
        /// The ratio.
        /// </value>
        [XmlAttribute]
        public float Ratio { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExplorationRatioValue"/> class.
        /// </summary>
        public ExplorationRatioValue()
            : base()
        {
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override float GetValue(IBehaviorContext context)
        {
            context.AddLogMessage("ExplorationRatioValue");
            PlayerBehaviorContext playerContext = context as PlayerBehaviorContext;
            PlanetBehaviorContext planetContext = context as PlanetBehaviorContext;
            if (playerContext == null && planetContext == null)
            {
                Debug.Assert(false, "Wrong Behavior Context");
                return 0;
            }

            Player player = null;            
            if (playerContext != null)
            {
                player = playerContext.Player;                
            }
            else
            {
                player = planetContext.Planet.Owner;
            }            

            float scoutUnits = player.Units.Count(u => u is Scout);            
            float value = scoutUnits / ((player.GameSession.Galaxy.TotalSystems - player.TotalExploredStarSystems) / Ratio);

            return value;
        }
    }
}
