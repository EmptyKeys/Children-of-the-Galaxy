using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using EmptyKeys.Strategy.Environment.Factory;
using EmptyKeys.Strategy.Environment.PlanetUpgrades;

namespace EmptyKeys.Strategy.AI.Components.Decisions
{
    /// <summary>
    /// Implements planet decision leaf for Decision tree behavior. This decision calculates value of possible planet upgrade (UpgradeModifier type).
    /// If this decision is selected, Factory Item is stored in ItemToBuild of PlanetBehaviorContext.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.BaseDecisionTreeNode" />
    public class PlanetDecisionLeaf : BaseDecisionTreeNode
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
        /// Initializes a new instance of the <see cref="PlanetDecisionLeaf"/> class.
        /// </summary>
        public PlanetDecisionLeaf()
            : base()
        {
        }

        /// <summary>
        /// Calculates the value.
        /// </summary>
        /// <param name="context">The context.</param>
        public override void CalculateValue(IBehaviorContext context)
        {
            PlanetBehaviorContext planetContext = context as PlanetBehaviorContext;
            if (planetContext == null)
            {
                Value = 0;
                IsNodeValid = false;
                return;
            }
            
            List<FactoryItem> possibleUpgrades = planetContext.Planet.Owner.GetPossiblePlanetUpgrades(planetContext.Planet, UpgradeModifier);
            if (possibleUpgrades.Count == 0)
            {
                IsNodeValid = false;
                return;
            }

            IsNodeValid = true;
            Value = planetContext.Planet.Upgrades.Where(u => u.ModifierType == UpgradeModifier).Count();
        }

        /// <summary>
        /// Makes the decision.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override bool MakeDecision(IBehaviorContext context)
        {
            PlanetBehaviorContext planetContext = context as PlanetBehaviorContext;
            if (planetContext == null)
            {
                return false;
            }

            List<FactoryItem> possibleUpgrades = planetContext.Planet.Owner.GetPossiblePlanetUpgrades(planetContext.Planet, UpgradeModifier);
            if (possibleUpgrades.Count == 0)
            {
                return false;
            }

            FactoryItem upgrade = possibleUpgrades.OrderBy(u => u.ProductionCost).First();
            if (upgrade != null)
            {
                context.AddLogMessage($"Upgrade found - {upgrade.Name}");
                planetContext.ItemToBuild = upgrade;
                return true;
            }

            return false;
        }
    }
}
