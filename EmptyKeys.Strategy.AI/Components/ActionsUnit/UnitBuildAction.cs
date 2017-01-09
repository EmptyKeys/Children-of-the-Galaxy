using System.Linq;
using System.Xml.Serialization;
using EmptyKeys.Strategy.Environment.Factory;
using EmptyKeys.Strategy.Units;
using EmptyKeys.Strategy.Units.Tasks;

namespace EmptyKeys.Strategy.AI.Components.Actions
{
    /// <summary>
    /// Implements unit build action for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class UnitBuildAction : BehaviorComponentBase
    {
        /// <summary>
        /// Gets or sets the name of the unit type.
        /// </summary>
        /// <value>
        /// The name of the unit type.
        /// </value>
        [XmlAttribute]
        public string UnitTypeName { get; set; }

        /// <summary>
        /// Gets or sets the name of the unit behavior.
        /// </summary>
        /// <value>
        /// The name of the unit behavior.
        /// </value>
        [XmlAttribute]
        public string UnitBehaviorName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitBuildAction"/> class.
        /// </summary>
        public UnitBuildAction()
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
            UnitBehaviorContext unitContext = context as UnitBehaviorContext;
            if (unitContext == null || string.IsNullOrEmpty(UnitTypeName))
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            Builder unit = unitContext.Unit as Builder;
            if (unit == null || !unit.CanBuild)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            FactoryItem item = unit.Owner.AvailFactoryItems.FirstOrDefault(i => i.FactoryTypeName.EndsWith(UnitTypeName));
            if (item == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            bool canAcquire = item.Acquire(unit.Owner, unit);
            if (!canAcquire)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            BuilderUnitFactoryTask factoryTask = new BuilderUnitFactoryTask(item, unit);
            unit.ActiveFactoryTask = factoryTask;
            if (!string.IsNullOrEmpty(UnitBehaviorName))
            {                
                factoryTask.ItemBehaviorName = UnitBehaviorName;
            }

            BuildTask task = new BuildTask(unit);
            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
