using System.Xml.Serialization;

namespace EmptyKeys.Strategy.AI.Components.ActionsPlayer
{
    /// <summary>
    /// Implements player action for behavior. This action sets EnvironmentTarget of the unit in UnitBehaviorContext.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class PlayerSetUnitsEnvTarget : BehaviorComponentBase
    {
        /// <summary>
        /// Gets or sets the name of the behavior.
        /// </summary>
        /// <value>
        /// The name of the behavior.
        /// </value>
        [XmlAttribute]
        public string BehaviorName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerSetUnitsEnvTarget"/> class.
        /// </summary>
        public PlayerSetUnitsEnvTarget()
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

            foreach (var unit in playerContext.Player.Units)
            {
                if (unit.Behavior == null || 
                    unit.Behavior.Name != BehaviorName || 
                    unit.Environment == null ||
                    unit.HasTask())
                {
                    continue;
                }

                unit.BehaviorContext.EnvironmentTarget = playerContext.EnvironmentTarget;                
            }                                   

            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
