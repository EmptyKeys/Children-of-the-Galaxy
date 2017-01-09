using System.Linq;
using System.Xml.Serialization;
using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.ConditionsPlayer
{
    /// <summary>
    /// Implements player condition for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class PlayerHasUnitType : BehaviorComponentBase
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
        /// Initializes a new instance of the <see cref="PlayerHasUnitType"/> class.
        /// </summary>
        public PlayerHasUnitType()
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
            PlanetBehaviorContext planetContext = context as PlanetBehaviorContext;
            if (playerContext == null && planetContext == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            Player player = null;
            if (playerContext != null)
            {
                player = playerContext.Player;
            }
            else
            {
                player = planetContext.Planet.Owner;
            }

            if (player == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            BaseUnit existingUnit = player.Units.FirstOrDefault(u => u.GetType().Name == UnitTypeName);
            if (existingUnit != null)
            {                
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
