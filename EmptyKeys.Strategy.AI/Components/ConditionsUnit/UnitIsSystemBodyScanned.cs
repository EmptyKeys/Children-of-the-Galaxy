using EmptyKeys.Strategy.Environment;

namespace EmptyKeys.Strategy.AI.Components.ConditionsUnit
{
    /// <summary>
    /// Implements unit condition for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class UnitIsSystemBodyScanned : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitIsSystemBodyScanned"/> class.
        /// </summary>
        public UnitIsSystemBodyScanned()
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

            StarSystemBody target = unitContext.EnvironmentTarget as StarSystemBody;
            if (target == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            if (unitContext.Unit.Owner.ScannedStarSystemBodies.Contains(target.GlobalKey))
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
