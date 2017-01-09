using System;
using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Environment;
using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.Actions
{
    /// <summary>
    /// Implements unit action for behavior. This action is used by Builder AI to build star system defense.
    /// If there is Warp Magnet, it starts building defense around it as hexagon shape.
    /// The result (position) is stored in EnvironmentTarget of UnitBehaviorContext.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class FindDefensivePosition : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindDefensivePosition"/> class.
        /// </summary>
        public FindDefensivePosition()
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

            BaseEnvironment envi = unitContext.Unit.Environment;
            if (envi == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            bool foundPosition = false;
            HexElement position = new HexElement();
            StaticUnit warpMagnetUnit = BaseEnvironment.GetActiveWarpMagnet(envi) as StaticUnit;
            if (warpMagnetUnit == null)
            {                
                Builder unit = unitContext.Unit as Builder;
                position.Q = unit.Q;
                position.R = unit.R;
                foundPosition = true;
            }
            else
            {
                float distance = warpMagnetUnit.TotalSensorsEnergy + 1;
                foreach (var coord in HexMap.Neighbors)
                {
                    position.Q = (short)(warpMagnetUnit.Q + distance * coord.x);
                    position.R = (short)(warpMagnetUnit.R + distance * coord.y);
                    if (envi.UnitsMap.ContainsKey(position.HexMapKey) || !envi.PathFindingGraph.VertexHexCache.ContainsKey(position.HexMapKey))
                    {
                        continue;
                    }

                    foundPosition = true;
                    break;
                }
            }

            if (!foundPosition)
            {
                unitContext.EnvironmentTarget = null;
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            unitContext.EnvironmentTarget = position;
            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
