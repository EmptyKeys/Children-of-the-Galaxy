using System.Diagnostics;
using System.Xml.Serialization;
using EmptyKeys.Strategy.Environment.PlanetUpgrades;

namespace EmptyKeys.Strategy.AI.Components.Considerations
{
    /// <summary>
    /// Implements Consideration value for Utility based AI. This value represents number of possible planet upgrades of specified type (UpgradeModifier).
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.Considerations.BaseConsiderationValue" />
    public class PlanetUpgradesValue : BaseConsiderationValue
    {
        /// <summary>
        /// Gets or sets the upgrade modifier.
        /// </summary>
        /// <value>
        /// The upgrade modifier.
        /// </value>
        [XmlAttribute]
        public UpgradeModifierType UpgradeModifier { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlanetUpgradesValue"/> class.
        /// </summary>
        public PlanetUpgradesValue()
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
            context.AddLogMessage("PlanetUpgradesValue");
            PlanetBehaviorContext planetContext = context as PlanetBehaviorContext;
            if (planetContext == null)
            {
                Debug.Assert(false, "Wrong Behavior Context");
                return 0;
            }

            float value = planetContext.Planet.Owner.GetPossiblePlanetUpgrades(planetContext.Planet, UpgradeModifier).Count;

            return value;
        }
    }
}
