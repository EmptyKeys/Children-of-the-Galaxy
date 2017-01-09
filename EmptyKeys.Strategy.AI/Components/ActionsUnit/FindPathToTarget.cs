using System.Xml.Serialization;
using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.Actions
{
    /// <summary>
    /// Implements unit action for behavior. This action finds path of the unit to its Target so that Target is in sensor range.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class FindPathToTarget : BehaviorComponentBase
    {
        /// <summary>
        /// Gets or sets a value indicating whether [path target position].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [path target position]; otherwise, <c>false</c>.
        /// </value>
        [XmlAttribute]
        public bool PathTargetPosition { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FindPathToTarget"/> class.
        /// </summary>
        public FindPathToTarget()
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

            if (unit.Environment != unit.Target.Environment)
            {
                unit.Target = null;
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            /*
            Vector targetCoords = new Vector();
            
            Vector unitCoords = new Vector();
            unitCoords.X = unit.Q;
            unitCoords.Y = unit.R;

            Vector direction = Vector.Normalize(targetCoords - unitCoords);
            direction.X = (float)Math.Round(direction.X, 0);
            direction.Y = (float)Math.Round(direction.Y, 0);

            if (PathTargetPosition)
            {
                direction = Vector.Zero;
            }

            unit.SelectedPath = null;
            int range = (int) unit.SensorsEnergy;
            while (unit.SelectedPath == null && range > 0)
            {
                targetCoords -= (direction * range);
                unit.CalculatePath(unit.Environment, (short) targetCoords.X, (short) targetCoords.Y);
                range--;
            }
            */

            short nq = unit.Target.Q;
            short nr = unit.Target.R;            
            HexElement existingUnit = null;
            bool foundHex = false;
            int searchRadius = (int) unit.SensorsEnergy;
            short centerQ = nq;
            short centerR = nr;
            while (!foundHex && searchRadius > 0)
            {
                var range = HexMap.GetRing(centerQ, centerR, searchRadius);
                foreach (var coord in range)
                {
                    nq = coord.Item1;
                    nr = coord.Item2;
                    if (unit.Environment.MapRadius > HexMap.Distance(0, nq, 0, nr) &&
                        !unit.Environment.UnitsMap.TryGetValue(HexMap.CalculateKey(nq, nr), out existingUnit))
                    {
                        foundHex = true;
                        break;
                    }
                }

                searchRadius--;
            }

            if (foundHex)
            {
                unit.CalculatePath(unit.Environment, nq, nr);
            }            

            if (!foundHex || unit.SelectedPath == null)
            {
                unit.Target = null;
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
