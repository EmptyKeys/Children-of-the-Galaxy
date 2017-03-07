using System.Linq;
using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Environment;
using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.ActionsUnit
{
    /// <summary>
    /// Implements unit action for behavior. This action finds the nearest enemy planet with the lowest influence and free slot on orbit.
    /// The result is stored in EnvironmentTarget of UnitBehaviorContext.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class FindWeakestEnemyPlanet : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindWeakestEnemyPlanet"/> class.
        /// </summary>
        public FindWeakestEnemyPlanet()
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
            Planet weakestPlanet = null;
            float minUtility = float.MaxValue;
            foreach (var elem in unit.Environment.EnvironmentMap.Values)
            {
                Planet target = elem as Planet;
                if (target == null || target.Owner == player || target.Owner == null || !target.CanEnterMoreUnits(player))
                {
                    continue;
                }

                if (!(player.IsHostile(target.Owner) || target.Owner.IsHostile(player)))
                {
                    continue;
                }

                HexElement infElem;
                if (!unit.Environment.EnvironmentInfluenceMap.TryGetValue(target.HexMapKey, out infElem))
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
                    weakestPlanet = target;
                }
            }

            unitContext.EnvironmentTarget = weakestPlanet;
            if (weakestPlanet != null)
            {
                context.AddLogMessage("Target Planet found - " + weakestPlanet.Name);
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
