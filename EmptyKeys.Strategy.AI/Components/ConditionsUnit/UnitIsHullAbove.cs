using System.Xml.Serialization;

namespace EmptyKeys.Strategy.AI.Components.ConditionsUnit
{
    /// <summary>
    /// Implements unit condition for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class UnitIsHullAbove : BehaviorComponentBase
    {
        /// <summary>
        /// Gets or sets the hull percentage.
        /// </summary>
        /// <value>
        /// The hull percentage.
        /// </value>
        [XmlAttribute]
        public float HullPercentage { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitIsHullAbove"/> class.
        /// </summary>
        public UnitIsHullAbove()
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

            float currentPercentage = unitContext.Unit.Hull / unitContext.Unit.HullMax;
            if (HullPercentage < currentPercentage)
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
