namespace EmptyKeys.Strategy.AI.Components.Considerations
{
    /// <summary>
    /// Implements Consideration for Utility based AI. This consideration is constant value.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.Considerations.BaseConsideration" />
    public class Constant : BaseConsideration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Constant"/> class.
        /// </summary>
        public Constant()
            : base()
        {
        }

        /// <summary>
        /// Executes consideration
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Consider(IBehaviorContext context)
        {
            Rank = Value;

            base.Consider(context);
        }
    }
}
