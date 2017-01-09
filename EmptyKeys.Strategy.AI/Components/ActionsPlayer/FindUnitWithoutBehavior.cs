using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using EmptyKeys.Strategy.Environment;
using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.ActionsPlayer
{
    /// <summary>
    /// Implements player action for behavior. This action finds specific unit of UnitTypeName, which is without behavior.
    /// The result is stored in BehaviorTarget of PlayerBehaviorContext.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class FindUnitWithoutBehavior : BehaviorComponentBase
    {
        /// <summary>
        /// Gets or sets the name of the unit type.
        /// </summary>
        /// <value>
        /// The name of the unit type.
        /// </value>
        [XmlAttribute]
        public string UnitTypeName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [asteroid check enabled].
        /// </summary>
        /// <value>
        /// <c>true</c> if [asteroid check enabled]; otherwise, <c>false</c>.
        /// </value>
        [XmlAttribute]
        public bool AsteroidCheckEnabled { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FindUnitWithoutBehavior"/> class.
        /// </summary>
        public FindUnitWithoutBehavior()
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
            PlayerBehaviorContext playerContext = context as PlayerBehaviorContext;
            if (playerContext == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            List<BaseUnit> existingUnits = playerContext.Player.Units.Where(u => u.GetType().Name == UnitTypeName && u.Behavior == null).ToList();
            if (existingUnits == null || existingUnits.Count == 0)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            playerContext.BehaviorTarget = null;
            foreach (var unit in existingUnits)
            {                          
                // this check is used for Builders, if there is not any available asteroid in the system, it ignores that unit
                if (AsteroidCheckEnabled)
                {
                    var asteroid = unit.Environment.EnvironmentMap.Values.FirstOrDefault(e => e is Asteroid && !((Asteroid)e).IsExtracted);
                    if (asteroid == null)
                    {
                        continue;
                    }
                }

                playerContext.BehaviorTarget = unit;
                break;
            }

            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
