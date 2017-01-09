using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace EmptyKeys.Strategy.AI.Components.Considerations
{
    /// <summary>
    /// Implements Consideration for Utility based AI. This consideration is Decay curve.
    /// Rank = (Pow(Max, K) - Pow(Value, K)) / Pow(Max, K)
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.Considerations.BaseConsideration" />
    public class DecayCurve : BaseConsideration
    {
        /// <summary>
        /// Gets or sets the maximum.
        /// </summary>
        /// <value>
        /// The maximum.
        /// </value>
        [XmlAttribute]
        public float Max { get; set; }

        /// <summary>
        /// </summary>
        /// <value>
        /// The k.
        /// </value>
        [XmlAttribute]
        public float K { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DecayCurve"/> class.
        /// </summary>
        public DecayCurve()
            : base()
        {
        }

        /// <summary>
        /// Executes consideration
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Consider(IBehaviorContext context)
        {
            if (ValueProvider == null)
            {
                Debug.Assert(false, "Missing Value Provider");
                return;
            }

            Value = ValueProvider.GetValue(context);

            Rank = (float)((Math.Pow(Max, K) - Math.Pow(Value, K)) / Math.Pow(Max, K));
            base.Consider(context);
        }
    }
}
