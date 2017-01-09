using System.Linq;
using EmptyKeys.Strategy.Core;

namespace EmptyKeys.Strategy.AI.Components.ActionsPlayer
{
    /// <summary>
    /// Implements player action for behavior. This action creates RelationValues enumerator for PlayerBehaviorContext and for each relation finds Player instance.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class PlayerGetAllPlayerRelations : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerGetAllPlayerRelations"/> class.
        /// </summary>
        public PlayerGetAllPlayerRelations()
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

            playerContext.RelationValues = playerContext.Player.RelationsValues.GetEnumerator();
            if (!playerContext.RelationValues.MoveNext())
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            foreach (var relation in playerContext.Player.RelationsValues)
            {
                Player otherPlayer = playerContext.Player.GameSession.Players.FirstOrDefault(p => p.Index == relation.PlayerIndex);
                relation.Player = otherPlayer;
            }            
           
            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
