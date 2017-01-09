using System.Collections.ObjectModel;
using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.ConditionsPlanet
{
    /// <summary>
    /// Implements planet condition for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class PlanetIsUnitOnOrbit : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlanetIsUnitOnOrbit"/> class.
        /// </summary>
        public PlanetIsUnitOnOrbit()
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

            ObservableCollection<MoveableUnit> playersUnitsOnOrbit = null;
            if (!planetContext.Planet.Orbit.Units.TryGetValue(planetContext.Planet.Owner, out playersUnitsOnOrbit))
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }
            
            if (playersUnitsOnOrbit.Count > 0)
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
