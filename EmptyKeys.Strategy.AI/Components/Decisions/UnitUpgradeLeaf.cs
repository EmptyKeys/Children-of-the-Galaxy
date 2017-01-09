using System.Diagnostics;
using System.Linq;
using System.Xml.Serialization;
using EmptyKeys.Strategy.Units;
using EmptyKeys.Strategy.Units.Upgrades;

namespace EmptyKeys.Strategy.AI.Components.Decisions
{
    /// <summary>
    /// Implements player decision leaf for Decision tree behavior. This decision calculates value for specified upgrade (UpgradeId).
    /// If this decision is selected, upgrade is stored in UnitUpgrade of PlayerBehaviorContext.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.BaseDecisionTreeNode" />
    public class UnitUpgradeLeaf : BaseDecisionTreeNode
    {
        /// <summary>
        /// Gets or sets the upgrade identifier.
        /// </summary>
        /// <value>
        /// The upgrade identifier.
        /// </value>
        [XmlAttribute]
        public byte UpgradeId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitUpgradeLeaf"/> class.
        /// </summary>
        public UnitUpgradeLeaf()
            : base()
        {
        }

        /// <summary>
        /// Calculates the value.
        /// </summary>
        /// <param name="context">The context.</param>
        public override void CalculateValue(IBehaviorContext context)
        {
            PlayerBehaviorContext playerContext = context as PlayerBehaviorContext;
            if (playerContext == null)
            {
                IsNodeValid = false;
                Debug.Assert(false, "Player Context null");
                return;
            }

            BaseUnit unit = playerContext.Unit;
            if (unit == null)
            {
                IsNodeValid = false;
                Debug.Assert(false, "Unit null");
                return;
            }

            if (!playerContext.Player.AvailUnitUpgrades.Contains(UpgradeId))
            {
                IsNodeValid = false;
                return;
            }

            BaseUnitUpgrade upgrade = playerContext.Player.GameSession.EnvironmentConfig.UpgradesConfig.Upgrades.FirstOrDefault(u => u.Id == UpgradeId);
            if (upgrade == null)
            {
                IsNodeValid = false;
                return;
            }

            if (!upgrade.IsValidUpgrade(unit))
            {
                IsNodeValid = false;
                return;
            }

            int usedCount = unit.Upgrades.Count(u => u == upgrade);                       

            IsNodeValid = true;
            Value = usedCount;
        }

        /// <summary>
        /// Makes the decision.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override bool MakeDecision(IBehaviorContext context)
        {
            PlayerBehaviorContext playerContext = context as PlayerBehaviorContext;
            if (playerContext == null)
            {                
                return false;
            }

            BaseUnitUpgrade upgrade = playerContext.Player.GameSession.EnvironmentConfig.UpgradesConfig.Upgrades.FirstOrDefault(u => u.Id == UpgradeId);
            if (upgrade == null)
            {
                return false;
            }

            playerContext.UnitUpgrade = upgrade;

            return true;
        }        
    }
}
