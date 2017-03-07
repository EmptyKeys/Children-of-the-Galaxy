using System;
using System.Collections.Generic;
using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Environment;
using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.ActionsUnit
{
    /// <summary>
    /// Implements unit action for behavior. This action finds the enemy unit in unit's sensor range.
    /// The result is stored in Target of Unit.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class FindEnemyTargetInRange : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindEnemyTargetInRange"/> class.
        /// </summary>
        public FindEnemyTargetInRange()
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

            StaticUnit unit = unitContext.Unit as StaticUnit;
            if (unit == null || unit.IsDead)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            BaseEnvironment envi = unit.Environment;
            if (envi == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }            

            Player player = unit.Owner;
            unit.Target = null;
            List<Tuple<short, short>> sensorRange = HexMap.GetRange(unit.Q, unit.R, (int)unit.SensorsEnergy);
            foreach (var hexCoords in sensorRange)
            {
                int key = HexMap.CalculateKey(hexCoords.Item1, hexCoords.Item2);
                HexElement existingElem;
                if (!envi.UnitsMap.TryGetValue(key, out existingElem))
                {
                    continue;
                }

                BaseUnit target = existingElem as BaseUnit;
                if (target == null || target.Owner == player || target.Owner == null || target.IsDead)
                {
                    continue;
                }
                
                if (!(player.IsHostile(target.Owner) || target.Owner.IsHostile(player)))
                {
                    continue;
                }

                unit.Target = target;
                break;
            }

            if (unit.Target != null)
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
