using System.Xml.Serialization;
using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.ActionsPlayer
{
    /// <summary>
    /// Implements player action for behavior. This action sets Unit (BehaviorTarget) behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class PlayerSetUnitBehavior : BehaviorComponentBase
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
        /// Initializes a new instance of the <see cref="PlayerSetUnitBehavior"/> class.
        /// </summary>
        public PlayerSetUnitBehavior()
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

            BaseUnit unit = playerContext.BehaviorTarget as BaseUnit;
            if (unit == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            Behavior behavior = null;
            if (playerContext.Behavior != null)
            {
                behavior = playerContext.Behavior;
            }
            else
            {
                if (string.IsNullOrEmpty(BehaviorName))
                {
                    returnCode = BehaviorReturnCode.Failure;
                    return returnCode;
                }

                behavior = BehaviorsManager.Instance.GetBehavior(BehaviorName);
            }
            
            if (behavior == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            context.AddLogMessage($"Unit {unit.Name} Set Behavior {behavior.Name}");  //string.Format("Unit {0} Set Behavior {1}", unit.Name, behavior.Name));
            unit.Behavior = behavior;
            unit.BehaviorContext.EnvironmentTarget = null;            
            
            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
