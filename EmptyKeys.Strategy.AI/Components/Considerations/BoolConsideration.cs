using System.Diagnostics;
using System.Xml.Serialization;

namespace EmptyKeys.Strategy.AI.Components.Considerations
{
    /// <summary>
    /// Implements Boolean consideration for utility based AI.
    /// If Value is higher than TrueBoundary, it sets Multiplier to 1, else 0. Rank is always 0.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.Considerations.BaseConsideration" />
    public class BoolConsideration : BaseConsideration
    {
        /// <summary>
        /// Gets or sets the true boundary.
        /// </summary>
        /// <value>
        /// The true boundary.
        /// </value>
        [XmlAttribute]
        public float TrueBoundary { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoolConsideration"/> class.
        /// </summary>
        public BoolConsideration()
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

            if (Value > TrueBoundary)
            {
                Rank = 0;
                Multiplier = 1;
            }
            else
            {
                Rank = 0;
                Multiplier = 0;
            }
            
            base.Consider(context);
        }
    }
}
