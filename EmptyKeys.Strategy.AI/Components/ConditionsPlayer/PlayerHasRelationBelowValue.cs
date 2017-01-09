using System.Xml.Serialization;

namespace EmptyKeys.Strategy.AI.Components.ConditionsPlayer
{
    /// <summary>
    /// Implements player condition for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class PlayerHasRelationBelowValue : BehaviorComponentBase
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
        /// Initializes a new instance of the <see cref="PlayerHasRelationBelowValue"/> class.
        /// </summary>
        public PlayerHasRelationBelowValue()
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
            if (playerContext == null || playerContext.RelationValues == null || playerContext.RelationValues.Current == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            var relationValue = playerContext.RelationValues;
            if (RelationValue > relationValue.Current.RelationValue)
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
