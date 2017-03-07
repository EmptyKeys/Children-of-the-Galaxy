using System.Linq;
using System.Xml.Serialization;
using EmptyKeys.Strategy.Environment;

namespace EmptyKeys.Strategy.AI.Components.Decisions
{
    /// <summary>
    /// Implements planet decision leaf for Decision tree behavior. This decision calculates value of Unit type (UnitTypeName) with specific Behavior (BehaviorName).
    /// If this decision is selected, planet is set to build it (ItemToBuild).
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.BaseDecisionTreeNode" />
    public class PlayerUnitDecisionLeaf : BaseDecisionTreeNode
    {
        /// <summary>
        /// Gets or sets the name of the unit type.
        /// </summary>
        /// <value>
        /// The name of the unit type.
        /// </value>
        [XmlAttribute]
        public string UnitTypeName { get; set; }

        /// <summary>
        /// Gets or sets the name of the behavior.
        /// </summary>
        /// <value>
        /// The name of the behavior.
        /// </value>
        [XmlAttribute]
        public string BehaviorName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerUnitDecisionLeaf"/> class.
        /// </summary>
        public PlayerUnitDecisionLeaf()
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
                Value = 0;
                IsNodeValid = false;
                return;
            }            

            var itemToBuild = playerContext.Player.AvailFactoryItems.FirstOrDefault(i => i.FactoryTypeName.EndsWith(UnitTypeName));
            if (itemToBuild == null)
            {
                Value = 0;
                IsNodeValid = false;
                return;
            }

            IsNodeValid = true;
            Value = playerContext.Player.Units.Count(u => u.GetType().Name == UnitTypeName && u.Behavior != null && u.Behavior.Name == BehaviorName);
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

            Planet planet = playerContext.BehaviorTarget as Planet;
            if (planet == null)
            {                
                return false;
            }

            PlanetBehaviorContext planetContext = planet.BehaviorContext as PlanetBehaviorContext;
            if (planetContext == null)
            {                
                return false;
            }            

            planetContext.ItemToBuild = playerContext.Player.AvailFactoryItems.FirstOrDefault(i => i.FactoryTypeName.EndsWith(UnitTypeName));
            if (planetContext.ItemToBuild == null)
            {
                return false;
            }

            planetContext.ItemBehaviorName = BehaviorName;
            context.AddLogMessage($"Player Unit Decision - {UnitTypeName}");

            return true;
        }
    }
}
