using System.Diagnostics;
using System.Linq;
using System.Xml.Serialization;
using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.Decisions
{
    /// <summary>
    /// Implements unit decision leaf for Decision tree behavior. This decision calculates value of specified behavior (BehaviorName) for all player's units.
    /// If this decision is selected, it sets Unit behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.BaseDecisionTreeNode" />
    public class UnitBehaviorLeaf : BaseDecisionTreeNode
    {
        /// <summary>
        /// Gets or sets the name of the behavior.
        /// </summary>
        /// <value>
        /// The name of the behavior.
        /// </value>
        [XmlAttribute]
        public string BehaviorName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitBehaviorLeaf"/> class.
        /// </summary>
        public UnitBehaviorLeaf()
            : base()
        {
        }

        /// <summary>
        /// Calculates the value.
        /// </summary>
        /// <param name="context">The context.</param>
        public override void CalculateValue(IBehaviorContext context)
        {
            BaseUnit unit = GetUnitFromContext(context);
            if (unit == null)
            {
                Value = 0;
                IsNodeValid = false;
                return;
            }

            IsNodeValid = true;
            Value = unit.Owner.Units.Count(u => u.Behavior != null && u.Behavior.Name == BehaviorName);
        }

        /// <summary>
        /// Makes the decision.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override bool MakeDecision(IBehaviorContext context)
        {
            BaseUnit unit = GetUnitFromContext(context);
            if (unit == null)
            {
                return false;
            }

            Behavior behavior = null;
            if (!string.IsNullOrEmpty(BehaviorName))
            {
                behavior = BehaviorsManager.Instance.GetBehavior(BehaviorName);
                if (behavior == null)
                {
                    Debug.Assert(false, "Behavior does not exist");
                    return false;
                }
            }

            unit.Behavior = behavior;
            context.AddLogMessage($"Unit Decision Behavior - {BehaviorName}");

            return true;
        }

        private static BaseUnit GetUnitFromContext(IBehaviorContext context)
        {
            UnitBehaviorContext unitContext = context as UnitBehaviorContext;
            PlayerBehaviorContext playerContext = context as PlayerBehaviorContext;
            if (unitContext == null && playerContext == null)
            {
                return null;
            }

            BaseUnit unit = null;
            if (unitContext != null)
            {
                unit = unitContext.Unit;
            }
            else if (playerContext != null)
            {
                unit = playerContext.BehaviorTarget as BaseUnit;
            }

            return unit;
        }
    }
}
