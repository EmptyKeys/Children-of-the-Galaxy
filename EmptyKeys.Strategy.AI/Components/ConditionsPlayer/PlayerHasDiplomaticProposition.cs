using EmptyKeys.Strategy.Diplomacy;
using System.Linq;
using System.Xml.Serialization;

namespace EmptyKeys.Strategy.AI.Components.ConditionsPlayer
{

    /// <summary>
    /// Implements player condition for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class PlayerHasDiplomaticProposition : BehaviorComponentBase
    {

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        [XmlAttribute]
        public DiplomaticActions Action { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerHasDiplomaticProposition" /> class.
        /// </summary>
        public PlayerHasDiplomaticProposition()
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
            if (playerContext?.RelationValues?.Current?.Player == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            var relationValue = playerContext.RelationValues.Current.Player.RelationsValues.FirstOrDefault(r => playerContext.Player.Index == r.PlayerIndex);
            if (relationValue.PropositionState.HasFlag(Action))
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
