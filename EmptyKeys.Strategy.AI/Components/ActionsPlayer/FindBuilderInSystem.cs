using System.Linq;
using EmptyKeys.Strategy.Environment;
using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.ActionsPlayer
{
    /// <summary>
    /// Implements player action for behavior. This action finds existing Builder unit in EnvironmentTarget, which is without behavior.
    /// The result is stored in BehaviorTarget of PlayerBehaviorContext.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class FindBuilderInSystem : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindBuilderInSystem"/> class.
        /// </summary>
        public FindBuilderInSystem()
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
            PlayerBehaviorContext playerContext = context as PlayerBehaviorContext;
            if (playerContext == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            BaseEnvironment envi = playerContext.EnvironmentTarget as BaseEnvironment;
            if (envi == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            BaseUnit existingUnit = playerContext.Player.Units.FirstOrDefault(u => u.Environment == envi && u is Builder && u.Behavior == null);
            playerContext.BehaviorTarget = existingUnit;
            if (existingUnit != null)
            {                
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
