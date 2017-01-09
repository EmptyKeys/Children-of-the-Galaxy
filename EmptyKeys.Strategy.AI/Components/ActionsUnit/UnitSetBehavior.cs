using System.Xml.Serialization;

namespace EmptyKeys.Strategy.AI.Components.Actions
{
    /// <summary>
    /// Implements action, which sets Behavior of the unit.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class UnitSetBehavior : BehaviorComponentBase
    {
        /// <summary>
        /// Gets or sets the name of the behavior.
        /// </summary>
        /// <value>
        /// The name of the behavior.
        /// </value>
        [XmlAttribute]
        public string BehaviorName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitSetBehavior"/> class.
        /// </summary>
        public UnitSetBehavior()
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
            if (unitContext == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            Behavior behavior = null;
            if (!string.IsNullOrEmpty(BehaviorName))
            {
                behavior = BehaviorsManager.Instance.GetBehavior(BehaviorName);
                if (behavior == null)
                {
                    returnCode = BehaviorReturnCode.Failure;
                    return returnCode;
                }
            }

            unitContext.Unit.Behavior = behavior;

            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
