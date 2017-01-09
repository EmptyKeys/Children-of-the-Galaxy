using EmptyKeys.Strategy.Units.Tasks;

namespace EmptyKeys.Strategy.AI.Components.ActionsPlayer
{
    /// <summary>
    /// Implements player action for behavior. This action upgrades Unit with selected UnitUpgrade.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class PlayerUpgradeUnit : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerUpgradeUnit"/> class.
        /// </summary>
        public PlayerUpgradeUnit()
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

            if (playerContext.Unit == null || playerContext.UnitUpgrade == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            UpgradeTask task = new UpgradeTask(playerContext.Unit, playerContext.UnitUpgrade);
            task.Execute();

            if (task.IsTaskFinished)
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
