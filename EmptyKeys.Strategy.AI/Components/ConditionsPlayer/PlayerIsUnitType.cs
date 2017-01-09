using System.Xml.Serialization;

namespace EmptyKeys.Strategy.AI.Components.ConditionsPlayer
{
    /// <summary>
    /// Implements player condition for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class PlayerIsUnitType : BehaviorComponentBase
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
        /// Initializes a new instance of the <see cref="PlayerIsUnitType"/> class.
        /// </summary>
        public PlayerIsUnitType()
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

            if (playerContext.Unit.GetType().Name == UnitTypeName)
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
