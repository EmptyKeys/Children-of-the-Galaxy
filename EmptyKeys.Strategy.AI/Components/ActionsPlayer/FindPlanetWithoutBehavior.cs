using System.Linq;
using EmptyKeys.Strategy.Environment;

namespace EmptyKeys.Strategy.AI.Components.ActionsPlayer
{
    /// <summary>
    /// Implements player action for behavior. This action finds Planet without behavior.
    /// The result is stored in BehaviorTarget of PlayerBehaviorContext.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class FindPlanetWithoutBehavior : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindPlanetWithoutBehavior"/> class.
        /// </summary>
        public FindPlanetWithoutBehavior()
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

            Planet planet = playerContext.Player.StarSystemBodies.FirstOrDefault(b => b is Planet && ((Planet)b).Behavior == null) as Planet;
            playerContext.BehaviorTarget = planet;
            if (planet != null)
            {                
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
