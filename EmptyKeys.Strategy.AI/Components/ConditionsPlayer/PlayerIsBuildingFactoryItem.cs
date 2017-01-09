using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.ConditionsPlayer
{
    /// <summary>
    /// Implements player condition for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class PlayerIsBuildingFactoryItem : BehaviorComponentBase
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
        /// Initializes a new instance of the <see cref="PlayerIsBuildingFactoryItem"/> class.
        /// </summary>
        public PlayerIsBuildingFactoryItem()
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

            List<BaseUnit> builders = playerContext.Player.Units.Where(u => u is Builder).ToList();
            if (builders.Count == 0)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            foreach (var unit in builders)
            {
                Builder builder = unit as Builder;
                if (builder == null || builder.ActiveFactoryTask == null)
                {
                    continue;
                }

                if (builder.ActiveFactoryTask.Item.Id != FactoryItemId)
                {
                    continue;
                }

                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
