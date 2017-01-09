using System.Linq;
using System.Xml.Serialization;
using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Research;

namespace EmptyKeys.Strategy.AI.Components.Decisions
{
    /// <summary>
    /// Implements player decision leaf for Decision tree behavior. This decision calculates research technology goal value of specified technology (GoalTechnologyId).
    /// If this goal is selected, the result is stored in GoalTechnologyId of PlayerBehaviorContext.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.BaseDecisionTreeNode" />
    public class ResearchGoalLeaf : BaseDecisionTreeNode
    {
        /// <summary>
        /// Gets or sets the goal technology identifier.
        /// </summary>
        /// <value>
        /// The goal technology identifier.
        /// </value>
        [XmlAttribute]
        public int GoalTechnologyId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResearchGoalLeaf"/> class.
        /// </summary>
        public ResearchGoalLeaf()
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

            IsNodeValid = true;
            Player player = playerContext.Player;
            Technology tech = player.Technologies.FirstOrDefault(t => t.Data.Id == GoalTechnologyId);
            if (tech.IsAquired)
            {
                Value = 0;
                IsNodeValid = false;
                return;
            }

            Value = GetGoalCurrentValue(player, tech);
        }

        private float GetGoalCurrentValue(Player player, Technology tech)
        {
            float result = tech.Progress;

            foreach (var parentId in tech.Data.Parents)
            {
                Technology parentTech = player.Technologies.FirstOrDefault(t => t.Data.Id == parentId);
                result += GetGoalCurrentValue(player, parentTech);
            }

            return result;
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

            playerContext.GoalTechnologyId = GoalTechnologyId;
            context.AddLogMessage($"Research Decision - {GoalTechnologyId}");

            return true;
        }
    }
}
