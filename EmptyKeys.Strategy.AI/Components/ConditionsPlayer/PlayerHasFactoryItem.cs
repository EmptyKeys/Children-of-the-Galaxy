using System.Linq;
using System.Xml.Serialization;
using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Environment.Factory;

namespace EmptyKeys.Strategy.AI.Components.ConditionsPlayer
{
    /// <summary>
    /// Implements player condition for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class PlayerHasFactoryItem : BehaviorComponentBase
    {
        /// <summary>
        /// Gets or sets the factory item identifier.
        /// </summary>
        /// <value>
        /// The factory item identifier.
        /// </value>
        [XmlAttribute]
        public int FactoryItemId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerHasFactoryItem"/> class.
        /// </summary>
        public PlayerHasFactoryItem()
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
            if (playerContext == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }
            
            Player player = playerContext.Player;
            if (player == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            FactoryItem existingFactoryItem = player.AvailFactoryItems.FirstOrDefault(f => f.Id == FactoryItemId);
            if (existingFactoryItem != null)
            {                
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
