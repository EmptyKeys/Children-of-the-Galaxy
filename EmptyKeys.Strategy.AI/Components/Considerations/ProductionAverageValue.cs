using System.Diagnostics;

namespace EmptyKeys.Strategy.AI.Components.Considerations
{
    /// <summary>
    /// Implements Consideration value for Utility based AI. This value represents production average of the context Player.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.Considerations.BaseConsiderationValue" />
    public class ProductionAverageValue : BaseConsiderationValue
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductionAverageValue"/> class.
        /// </summary>
        public ProductionAverageValue()
            : base()
        {
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override float GetValue(IBehaviorContext context)
        {
            context.AddLogMessage("ProductionAverageValue");
            PlayerBehaviorContext playerContext = context as PlayerBehaviorContext;
            if (playerContext == null)
            {
                Debug.Assert(false, "Wrong Behavior Context");
                return 0;
            }

            float value = playerContext.Player.TotalProduction / playerContext.Player.TotalPlanets;

            return value;
        }
    }
}
