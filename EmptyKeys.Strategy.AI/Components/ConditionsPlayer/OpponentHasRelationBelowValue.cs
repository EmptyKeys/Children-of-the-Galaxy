using System.Linq;
using System.Xml.Serialization;
using EmptyKeys.Strategy.Core;

namespace EmptyKeys.Strategy.AI.Components.ConditionsPlayer
{
    /// <summary>
    /// Implements player condition for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class OpponentHasRelationBelowValue : BehaviorComponentBase
    {
        /// <summary>
        /// Gets or sets the relation value.
        /// </summary>
        /// <value>
        /// The relation value.
        /// </value>
        [XmlAttribute]
        public int RelationValue { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpponentHasRelationBelowValue"/> class.
        /// </summary>
        public OpponentHasRelationBelowValue()
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
            if (playerContext == null || playerContext.RelationValues == null || playerContext.RelationValues.Current == null || playerContext.RelationValues.Current.Player == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            int playerIndex = playerContext.Player.Index;
            var relation = playerContext.RelationValues.Current.Player.RelationsValues.FirstOrDefault(r => r.PlayerIndex == playerIndex);
            if (relation == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            if (RelationValue > relation.RelationValue)
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
