using EmptyKeys.Strategy.Environment;
using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.ActionsUnit
{
    /// <summary>
    /// Implements unit begin siege action for behavior
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class UnitBeginSiegeAction : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitBeginSiegeAction"/> class.
        /// </summary>
        public UnitBeginSiegeAction()
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

            MoveableUnit unit = unitContext.Unit as MoveableUnit;
            if (unit == null || !unit.IsOnOrbit)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }            

            Planet orbitingPlanet = unit.GetOrbitingPlanet();
            if (orbitingPlanet != null && orbitingPlanet.Owner != null && orbitingPlanet.Owner != unit.Owner && !orbitingPlanet.IsUnderSiege)
            {
                orbitingPlanet.BeginSiege(unit.Owner);
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }
            
            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
