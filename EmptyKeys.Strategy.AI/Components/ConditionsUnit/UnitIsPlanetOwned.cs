using EmptyKeys.Strategy.Environment;

namespace EmptyKeys.Strategy.AI.Components.Conditions
{
    /// <summary>
    /// Implements unit condition for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class UnitIsPlanetOwned : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitIsPlanetOwned"/> class.
        /// </summary>
        public UnitIsPlanetOwned()
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

            Planet planet = unitContext.EnvironmentTarget as Planet;
            if (planet == null || planet.Owner == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }            

            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
