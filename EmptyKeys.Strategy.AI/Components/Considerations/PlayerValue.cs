using System;
using System.Diagnostics;
using System.Reflection;
using EmptyKeys.Strategy.Core;

namespace EmptyKeys.Strategy.AI.Components.Considerations
{
    /// <summary>
    /// Implements Consideration value for Utility based AI. This value represents Player value of specified Property. Implemented with reflection.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.Considerations.BaseConsiderationValue" />
    public class PlayerValue : BaseConsiderationValue
    {
        private static readonly Type playerType = typeof(Player);
        private PropertyInfo info;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerValue"/> class.
        /// </summary>
        public PlayerValue()
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
            context.AddLogMessage($"PlayerValue {Property}");
            PlayerBehaviorContext playerContext = context as PlayerBehaviorContext;
            if (playerContext == null)
            {
                Debug.Assert(false, "Wrong Behavior Context");
                return 0;
            }

            if (string.IsNullOrEmpty(Property))
            {
                Debug.Assert(false, "Property null or empty");
                return 0;
            }

            if (info == null || info.Name != Property)
            {
                info = playerType.GetRuntimeProperty(Property);
                if (info == null)
                {
                    Debug.Assert(false, "Wrong Property Name");
                    return 0;
                }
            }

            return Convert.ToSingle(info.GetValue(playerContext.Player));
        }
    }
}
