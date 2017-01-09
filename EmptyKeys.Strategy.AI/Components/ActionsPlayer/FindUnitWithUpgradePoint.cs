using System.Linq;
using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.ActionsPlayer
{
    /// <summary>
    /// Implements player action for behavior. This action finds unit with available upgrade point.
    /// The result is stored in Unit of PlayerBehaviorContext.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class FindUnitWithUpgradePoint : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindUnitWithUpgradePoint"/> class.
        /// </summary>
        public FindUnitWithUpgradePoint()
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

            BaseUnit existingUnit = playerContext.Player.Units.FirstOrDefault(u => u.UpgradePoints > 0 && !u.IsDead);
            playerContext.Unit = existingUnit;
            if (existingUnit != null)
            {
                playerContext.AddLogMessage($"Find Unit {existingUnit.Name}");
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
