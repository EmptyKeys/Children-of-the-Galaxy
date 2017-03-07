using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Environment;
using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.ActionsUnit
{
    /// <summary>
    /// Implements unit action for behavior. This action finds the closest empty space and path to that position.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class FindEmptySpaceEnvTarget : BehaviorComponentBase
    {
        /// <summary>
        /// Gets or sets the radius denominator.
        /// </summary>
        /// <value>
        /// The radius denominator.
        /// </value>
        [XmlAttribute]
        public int RadiusDenominator { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FindEmptySpaceEnvTarget"/> class.
        /// </summary>
        public FindEmptySpaceEnvTarget()
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

            if (unitContext.EnvironmentTarget == null)
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

            BaseEnvironment envi = unit.Environment;
            if (envi == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }
                        
            for (int i = 1; i < envi.MapRadius / RadiusDenominator; i++)
            {
                List<Tuple<short, short>> ring = HexMap.GetRing(unitContext.EnvironmentTarget.Q, unitContext.EnvironmentTarget.R, i);

                foreach (var coord in ring)
                {                                        
                    int key = HexMap.CalculateKey(coord.Item1, coord.Item2);
                    if (envi.EnvironmentMap.ContainsKey(key))
                    {
                        continue;
                    }
                    
                    unit.CalculatePath(envi, coord.Item1, coord.Item2);
                    if (unit.SelectedPath != null)
                    {
                        returnCode = BehaviorReturnCode.Success;
                        return returnCode;
                    }                    
                }
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
