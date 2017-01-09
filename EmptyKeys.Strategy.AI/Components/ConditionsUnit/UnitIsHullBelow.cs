using System.Xml.Serialization;

namespace EmptyKeys.Strategy.AI.Components.Conditions
{
    /// <summary>
    /// Implements unit condition for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class UnitIsHullBelow : BehaviorComponentBase
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
        /// Initializes a new instance of the <see cref="UnitIsHullBelow"/> class.
        /// </summary>
        public UnitIsHullBelow()
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
            if (HullPercentage > currentPercentage)
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
