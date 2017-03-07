using System.Diagnostics;
using System.Linq;
using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Diplomacy;
using EmptyKeys.Strategy.Environment;
using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.ConditionsUnit
{
    /// <summary>
    /// Implements unit condition for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class UnitIsUnknownPlayerInSystem : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitIsUnknownPlayerInSystem"/> class.
        /// </summary>
        public UnitIsUnknownPlayerInSystem()
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

            if (unitContext.Unit.Environment is Galaxy)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            bool found = false;
            foreach (var elem in unitContext.Unit.Environment.UnitsMap.Values)
            {
                BaseUnit unit = elem as BaseUnit;
                if (unit == null)
                {
                    continue;
                }

                if (unit.Owner == unitContext.Unit.Owner)
                {
                    continue;
                }

                found |= CheckRelation(unitContext, unit.Owner);
            }

            foreach (var elem in unitContext.Unit.Environment.EnvironmentMap.Values)
            {
                Planet planet = elem as Planet;
                if (planet == null)
                {
                    continue;
                }

                if (planet.Owner == unitContext.Unit.Owner || planet.Owner == null)
                {
                    continue;
                }

                found |= CheckRelation(unitContext, planet.Owner);
            }

            if (found)
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }

        private bool CheckRelation(UnitBehaviorContext unitContext, Player anotherPlayer)
        {            
            Debug.Assert(anotherPlayer != null, "Player null");

            BaseUnit unit = unitContext.Unit;
            PlayerRelationValue relation = unit.Owner.RelationsValues.FirstOrDefault(r => r.PlayerIndex == anotherPlayer.Index);
            if (relation != null)
            {
                return false;
            }
                        
            unit.Owner.AddRelation(anotherPlayer, unit);

            relation = anotherPlayer.RelationsValues.FirstOrDefault(r => r.PlayerIndex == unit.Owner.Index);
            if (relation != null)
            {
                return true;
            }

            anotherPlayer.AddRelation(unit.Owner, unit);

            return true;
        }
    }
}
