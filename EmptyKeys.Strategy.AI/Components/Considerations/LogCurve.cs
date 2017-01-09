using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace EmptyKeys.Strategy.AI.Components.Considerations
{
    /// <summary>
    /// Implements Consideration for Utility based AI. This consideration is Log curve.
    /// Rank = (Log(Value / (1 - Value)) + Middle) / Denominator
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.Considerations.BaseConsideration" />
    public class LogCurve : BaseConsideration
    {
        /// <summary>
        /// Gets or sets the denominator.
        /// </summary>
        /// <value>
        /// The denominator.
        /// </value>
        [XmlAttribute]
        public float Denominator { get; set; }

        /// <summary>
        /// Gets or sets the middle.
        /// </summary>
        /// <value>
        /// The middle.
        /// </value>
        [XmlAttribute]
        public float Middle { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogCurve"/> class.
        /// </summary>
        public LogCurve()
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

            Rank = (float)((Math.Log(Value / (1 - Value)) + Middle) / Denominator);
            base.Consider(context);
        }
    }
}
