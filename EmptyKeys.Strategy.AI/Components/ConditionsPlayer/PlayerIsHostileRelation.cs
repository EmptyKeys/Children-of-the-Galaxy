using EmptyKeys.Strategy.Diplomacy;

namespace EmptyKeys.Strategy.AI.Components.ConditionsPlayer
{
    /// <summary>
    /// Implements player condition for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class PlayerIsHostileRelation : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerIsHostileRelation"/> class.
        /// </summary>
        public PlayerIsHostileRelation()
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
            if (playerContext == null || playerContext.RelationValues == null || playerContext.RelationValues.Current == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            var relationValue = playerContext.RelationValues;
            if (relationValue.Current.Level == PlayerRelationsLevel.Hostile || relationValue.Current.Player.IsHostile(playerContext.Player))
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
