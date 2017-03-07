using System;
using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Units;
using EmptyKeys.UserInterface;

namespace EmptyKeys.Strategy.AI.Components.ActionsUnit
{
    /// <summary>
    /// Implements unit action for behavior. This action finds path for unit in opposite direction from the enemy.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class FindPathAwayFromTarget : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindPathAwayFromTarget"/> class.
        /// </summary>
        public FindPathAwayFromTarget()
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
            if (unit == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            if (unit.Target == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            Vector targetCoords = new Vector();
            targetCoords.X = unit.Target.Q;
            targetCoords.Y = unit.Target.R;
            Vector unitCoords = new Vector();
            unitCoords.X = unit.Q;
            unitCoords.Y = unit.R;

            Vector distance = targetCoords - unitCoords;
            Vector direction = Vector.Normalize(distance);
            direction.X = (float)Math.Round(direction.X, 0);
            direction.Y = (float)Math.Round(direction.Y, 0);
            
            float fleeDistance = unit.SensorsEnergy;
            StaticUnit targetWithRange = unit.Target as StaticUnit;
            if (targetWithRange != null)
            {
                // stay out of target sensor range
                fleeDistance = targetWithRange.SensorsEnergy;
            }

            PointF fleeCoords = new PointF();
            unit.SelectedPath = null;
            int directionIndex = 0;
            while (unit.SelectedPath == null && directionIndex < HexMap.Neighbors.Count)
            {
                fleeCoords.X = unitCoords.X - (direction.X * fleeDistance);
                fleeCoords.Y = unitCoords.Y - (direction.Y * fleeDistance);
                unit.CalculatePath(unit.Environment, fleeCoords);
                direction.X = HexMap.Neighbors[directionIndex].x;
                direction.Y = HexMap.Neighbors[directionIndex].y;
                directionIndex++;
            }

            if (unit.SelectedPath != null)
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
