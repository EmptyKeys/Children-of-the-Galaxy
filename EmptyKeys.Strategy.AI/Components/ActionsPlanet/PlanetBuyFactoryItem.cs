using System.Linq;
using System.Xml.Serialization;
using EmptyKeys.Strategy.Environment.Factory;

namespace EmptyKeys.Strategy.AI.Components.ActionsPlanet
{
    /// <summary>
    /// Implements planet action for behavior. This action buys Factory Item and sets behavior,
    /// if Factory Item is unit.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class PlanetBuyFactoryItem : BehaviorComponentBase
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
        /// Gets or sets the name of the behavior.
        /// </summary>
        /// <value>
        /// The name of the behavior.
        /// </value>
        [XmlAttribute]
        public string BehaviorName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlanetBuyFactoryItem"/> class.
        /// </summary>
        public PlanetBuyFactoryItem()
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
            PlanetBehaviorContext planetContext = context as PlanetBehaviorContext;
            if (planetContext == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            FactoryItem item = null;
            if (!string.IsNullOrEmpty(ItemTypeName))
            {
                item = planetContext.Planet.Owner.AvailFactoryItems.FirstOrDefault(i => i.FactoryTypeName.EndsWith(ItemTypeName));
            }
            else if (planetContext.ItemToBuild != null)
            {
                item = planetContext.ItemToBuild;
            }
            

            if (item == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            bool canAcquire = item.Acquire(planetContext.Planet.Owner, planetContext.Planet);
            if (!canAcquire)
            {
                planetContext.ItemToBuild = null;
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            FactoryTask task = new BuyFactoryTask(item, planetContext.Planet);
            if (string.IsNullOrEmpty(BehaviorName))
            {
                task.ItemBehaviorName = planetContext.ItemBehaviorName;
                planetContext.ItemBehaviorName = string.Empty;
            }
            else
            {
                task.ItemBehaviorName = BehaviorName;
            }

            task.ProcessTurn();
            if (!task.IsFinished)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            planetContext.ItemToBuild = null;

            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
