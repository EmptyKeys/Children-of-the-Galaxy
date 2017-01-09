using EmptyKeys.Strategy.Environment;
using EmptyKeys.Strategy.Units;
using EmptyKeys.Strategy.Units.Tasks;

namespace EmptyKeys.Strategy.AI.Components.Actions
{
    /// <summary>
    /// Implements unit enter orbit action for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class UnitEnterOrbitAction : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitEnterOrbitAction"/> class.
        /// </summary>
        public UnitEnterOrbitAction()
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
            if (unit == null || !unit.CanMove || unit.Environment is Galaxy)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }            

            Planet planet = null;
            bool canOrbitPlanet = false;
            if (unit.IsStrikeGroupMember)
            {
                if (unit.StrikeGroup.GroupLeader != null)
                {
                    planet = unit.StrikeGroup.GroupLeader.GetOrbitingPlanet();
                    canOrbitPlanet = planet != null;
                }
            }
            else
            {
                canOrbitPlanet = unit.Environment.IsPlanetNearby(unit, out planet);                
            }

            if (canOrbitPlanet)
            {
                canOrbitPlanet = planet.Orbit.CanEnterMoreUnits(unit.Owner);
            }

            if (!canOrbitPlanet)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            EnterOrbitTask task = new EnterOrbitTask(unit, planet);
            task.Execute();

            if (task.IsTaskFinished && unit.IsOnOrbit)
            {                
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }
            
            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }        
    }
}
