using System.Linq;
using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Environment;
using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.Actions
{
    /// <summary>
    /// Implements unit action for behavior. This action finds the nearest enemy unit with the lowest influence.
    /// The result is stored in Target of the Unit.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class FindWeakestEnemyTarget : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindWeakestEnemyTarget"/> class.
        /// </summary>
        public FindWeakestEnemyTarget()
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
            if (unit == null)
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

            Player player = unitContext.Unit.Owner;
            BaseUnit weakestUnitTarget = null;
            float minUtility = float.MaxValue;
            foreach (var elem in envi.UnitsMap.Values)
            {
                BaseUnit target = elem as BaseUnit;
                if (target == null || target.Owner == player || target.Owner == null || target.IsDead)
                {
                    continue;
                }

                if (!(player.IsHostile(target.Owner) || target.Owner.IsHostile(player)))
                {
                    continue;
                }

                HexElement infElem;
                if (!envi.UnitsInfluenceMap.TryGetValue(target.HexMapKey, out infElem))
                {
                    continue;
                }

                float utility = 0;
                InfluenceElement influence = infElem as InfluenceElement;
                if (influence != null)
                {
                    utility = influence.Value;
                }
                else
                {
                    MultiLayerElement layer = infElem as MultiLayerElement;
                    if (layer == null)
                    {
                        continue;
                    }

                    InfluenceElement targetInf = layer.Values.FirstOrDefault(inf => ((InfluenceElement)inf).Owner == target.Owner) as InfluenceElement;
                    if (targetInf == null)
                    {
                        continue;
                    }

                    utility = targetInf.Value;
                }

                utility += HexMap.Distance(unitContext.Unit, target);

                if (minUtility > utility)
                {
                    minUtility = utility;
                    weakestUnitTarget = target;
                }
            }

            unit.Target = weakestUnitTarget;
            if (weakestUnitTarget != null)
            {
                context.AddLogMessage("Target found - " + weakestUnitTarget.Name);
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
