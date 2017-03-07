using EmptyKeys.Strategy.Units;
using EmptyKeys.Strategy.Units.Tasks;

namespace EmptyKeys.Strategy.AI.Components.ActionsUnit
{
    /// <summary>
    /// Implements unit warp magnet action for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class UnitWarpMagnetAction : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitWarpMagnetAction"/> class.
        /// </summary>
        public UnitWarpMagnetAction()
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
            UnitBehaviorContext unitContext = context as UnitBehaviorContext;
            if (unitContext == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            IWarpMagnetUnit warpMagnet = unitContext.Unit as IWarpMagnetUnit;
            if (warpMagnet == null || !warpMagnet.CanUseWarpMagnet || warpMagnet.IsWarpMagnetActive)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            WarpMagnetTask task = new WarpMagnetTask(unitContext.Unit);
            task.Execute();
            if (warpMagnet.IsWarpMagnetActive) //-V3022
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
