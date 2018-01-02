using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Serialization;
using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Research;
using EmptyKeys.Strategy.Diplomacy;

namespace EmptyKeys.Strategy.AI.Components.ActionsPlayer
{
    /// <summary>
    /// Implements player action for behavior. This action acquires diplomacy item for current Relation.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class PlayerAcquireDiplomacyItem : BehaviorComponentBase
    {
        /// <summary>
        /// Gets or sets the goal technology identifier.
        /// </summary>
        /// <value>
        /// The goal technology identifier.
        /// </value>
        [XmlAttribute]
        public int DiplomacyItemId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerAcquireDiplomacyItem"/> class.
        /// </summary>
        public PlayerAcquireDiplomacyItem()
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
            if (playerContext?.Player == null || playerContext.RelationValues == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            Player player = playerContext.Player;
            PlayerRelationValue relation = playerContext.RelationValues.Current;
            if (relation == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            DiplomacyItem item = relation.DiplomaticItems.FirstOrDefault(i => i.Data.Id == DiplomacyItemId && i.IsEnabled && !i.IsAcquired);
            if (item == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }
            
            item.Acquire(relation);

            if (item.IsAcquired)
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }        
    }
}
