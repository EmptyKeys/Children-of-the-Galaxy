using System.Xml.Serialization;
using EmptyKeys.Strategy.Environment;

namespace EmptyKeys.Strategy.AI.Components.ActionsPlayer
{
    /// <summary>
    /// Implements player action for behavior. This action sets Planet (BehaviorTarget) behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class PlayerSetPlanetBehavior : BehaviorComponentBase
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
        /// Initializes a new instance of the <see cref="PlayerSetPlanetBehavior"/> class.
        /// </summary>
        public PlayerSetPlanetBehavior()
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

            Planet planet = playerContext.BehaviorTarget as Planet;
            if (planet == null)
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

            context.AddLogMessage("Behavior Name - " + behavior.Name);
            planet.Behavior = behavior;

            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
