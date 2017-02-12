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
    public class PlayerHasEnergyForUnitMaintenance : BehaviorComponentBase
    {
        /// <summary>
        /// Gets or sets the name of the item type.
        /// </summary>
        /// <value>
        /// The name of the item type.
        /// </value>
        [XmlAttribute]
        public string ItemTypeName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerHasEnergyForUnitMaintenance"/> class.
        /// </summary>
        public PlayerHasEnergyForUnitMaintenance()
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
            PlanetBehaviorContext planetContext = context as PlanetBehaviorContext;
            UnitBehaviorContext unitContext = context as UnitBehaviorContext;
            if (playerContext == null && planetContext == null && unitContext == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            Player player = null;
            FactoryItem item = null;
            if (playerContext != null)
            {
                player = playerContext.Player;
                item = player.AvailFactoryItems.FirstOrDefault(i => i.FactoryTypeName.EndsWith(ItemTypeName));
            }
            else if (planetContext != null)
            {
                player = planetContext.Planet.Owner;
                if (planetContext.ItemToBuild != null)
                {
                    item = planetContext.ItemToBuild;
                }
                else if (!string.IsNullOrEmpty(ItemTypeName))
                {
                    item = player.AvailFactoryItems.FirstOrDefault(i => i.FactoryTypeName.EndsWith(ItemTypeName));
                }
            }
            else if (unitContext != null)
            {
                player = unitContext.Unit.Owner;
                item = player.AvailFactoryItems.FirstOrDefault(i => i.FactoryTypeName.EndsWith(ItemTypeName));
            }


            if (player == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }                      

            if (item == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            float maintenance = item.UnitConfig.MaintenanceCost;
            if (player.EnergyIncome > maintenance && player.Energy > maintenance)
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
