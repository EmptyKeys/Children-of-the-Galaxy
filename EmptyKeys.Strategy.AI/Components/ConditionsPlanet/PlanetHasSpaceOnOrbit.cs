namespace EmptyKeys.Strategy.AI.Components.ConditionsPlanet
{
    /// <summary>
    /// Implements planet condition for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class PlanetHasSpaceOnOrbit : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlanetHasSpaceOnOrbit"/> class.
        /// </summary>
        public PlanetHasSpaceOnOrbit()
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
            PlanetBehaviorContext planetContext = context as PlanetBehaviorContext;
            if (planetContext == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }
            
            if (planetContext.Planet.Orbit.CanEnterMoreUnits(planetContext.Planet.Owner))
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
