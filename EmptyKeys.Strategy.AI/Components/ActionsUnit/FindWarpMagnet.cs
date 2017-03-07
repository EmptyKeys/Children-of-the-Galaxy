using EmptyKeys.Strategy.Environment;
using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.ActionsUnit
{
    /// <summary>
    /// Implements unit action for behavior. This action finds active warp magnet in the star system.
    /// The result is stored in EnvironmentTarget of UnitBehaviorContext.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class FindWarpMagnet : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindWarpMagnet"/> class.
        /// </summary>
        public FindWarpMagnet()
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

            BaseEnvironment envi = unitContext.Unit.Environment;
            if (envi is Galaxy || envi == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            BaseUnit warpMagnetUnit = BaseEnvironment.GetActiveWarpMagnet(envi);
            if (warpMagnetUnit == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            unitContext.EnvironmentTarget = warpMagnetUnit;
            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
