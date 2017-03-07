using System.Xml.Serialization;
using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Environment;
using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.ActionsUnit
{
    /// <summary>
    /// Implements unit action for behavior. This action finds path of the unit to EnvironmentTarget of UnitBehaviorContext.
    /// If EnvironmentTarget is star, it finds the closest point and path to that point. 
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class FindPathToEnvTarget : BehaviorComponentBase
    {
        /// <summary>
        /// Gets or sets a value indicating whether [use planet next position].
        /// </summary>
        /// <value>
        /// <c>true</c> if [use planet next position]; otherwise, <c>false</c>.
        /// </value>
        [XmlAttribute]
        public bool UsePlanetNextPosition { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FindPathToEnvTarget"/> class.
        /// </summary>
        public FindPathToEnvTarget()
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

            if (unitContext.EnvironmentTarget == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            short q = unitContext.EnvironmentTarget.Q;
            short r = unitContext.EnvironmentTarget.R;
            if (UsePlanetNextPosition && unitContext.EnvironmentTarget is Planet)
            {
                q = ((Planet)unitContext.EnvironmentTarget).NextTurnQ;
                r = ((Planet)unitContext.EnvironmentTarget).NextTurnR;
            }

            Star star = unitContext.EnvironmentTarget as Star;
            if (star != null)
            {
                int radius = unit.Owner.GameSession.EnvironmentConfig.SpectralTypeConfig.SpectralTypesMinMaxValues[(int)star.SpectralType].HexRadius + 1;
                var range = HexMap.GetRing(0, 0, radius);
                if (range != null)
                {
                    int minDistance = int.MaxValue;
                    foreach (var coord in range)
                    {
                        int distance = HexMap.Distance(unit.Q, coord.Item1, unit.R, coord.Item2);
                        if (distance > minDistance)
                        {
                            continue;
                        }

                        minDistance = distance;
                        q = coord.Item1;
                        r = coord.Item2;                        
                    }                    
                }
            }

            unit.CalculatePath(unit.Environment, q, r);
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
