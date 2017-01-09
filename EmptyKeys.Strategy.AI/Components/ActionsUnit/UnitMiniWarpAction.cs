using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Environment;
using EmptyKeys.Strategy.Units;
using EmptyKeys.Strategy.Units.Tasks;

namespace EmptyKeys.Strategy.AI.Components.Actions
{
    /// <summary>
    /// Implements unit mini warp action for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class UnitMiniWarpAction : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitMiniWarpAction"/> class.
        /// </summary>
        public UnitMiniWarpAction()
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
            if (unit == null || !unit.CanMiniJump)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            if (unitContext.EnvironmentTarget == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            short destinationQ = unitContext.EnvironmentTarget.Q;
            short destinationR = unitContext.EnvironmentTarget.R;

            Planet planet = unitContext.EnvironmentTarget as Planet;
            MoveableUnit destinationUnit = unitContext.EnvironmentTarget as MoveableUnit;
            if (planet != null)
            {
                destinationQ = planet.NextTurnQ;
                destinationR = planet.NextTurnR;
            }
            else if (destinationUnit != null)
            {
                // find hex around unit
                short nq = destinationUnit.Q;
                short nr = destinationUnit.R;
                destinationUnit.GetMiniJumpDestination(out nq, out nr);
                int key = HexMap.CalculateKey(nq, nr);
                HexElement existingUnit = null;
                var envi = destinationUnit.Environment;

                bool foundHex = false;
                int searchRadius = 1;
                short centerQ = nq;
                short centerR = nr;
                while (!foundHex)
                {
                    var range = HexMap.GetRing(centerQ, centerR, searchRadius);
                    foreach (var coord in range)
                    {
                        nq = coord.Item1;
                        nr = coord.Item2;
                        if (envi.MapRadius > HexMap.Distance(0, nq, 0, nr) &&
                            !envi.UnitsMap.TryGetValue(HexMap.CalculateKey(nq, nr), out existingUnit))
                        {
                            foundHex = true;
                            break;
                        }
                    }

                    searchRadius++;
                }

                if (!foundHex)
                {
                    returnCode = BehaviorReturnCode.Failure;
                    return returnCode;
                }
                
                destinationQ = nq;
                destinationR = nr;
            }
            else
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            MiniWarpJumpTask task = new MiniWarpJumpTask(unit, destinationQ, destinationR);
            task.Execute();

            if (task.IsTaskFinished)
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
