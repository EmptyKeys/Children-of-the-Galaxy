using System.Xml.Serialization;

namespace EmptyKeys.Strategy.AI.Components.ConditionsPlayer
{
    /// <summary>
    /// Implements player condition for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class PlayerHasWarScoreBelowValue : BehaviorComponentBase
    {
        /// <summary>
        /// Gets or sets the war score.
        /// </summary>
        /// <value>
        /// The war score.
        /// </value>
        [XmlAttribute]
        public int WarScore { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerHasWarScoreBelowValue"/> class.
        /// </summary>
        public PlayerHasWarScoreBelowValue()
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
            if (WarScore > relationValue.Current.WarScore)
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
