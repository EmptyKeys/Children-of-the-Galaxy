using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Serialization;
using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Research;

namespace EmptyKeys.Strategy.AI.Components.ActionsPlayer
{
    /// <summary>
    /// Implements player action for behavior. This action starts technology research based on goal technology.
    /// Process starts with researching all parent technologies if they are not Acquired.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class PlayerResearchTech : BehaviorComponentBase
    {
        /// <summary>
        /// Gets or sets the goal technology identifier.
        /// </summary>
        /// <value>
        /// The goal technology identifier.
        /// </value>
        [XmlAttribute]
        public int GoalTechnologyId { get; set; }

        /// <summary>
        /// Gets or sets the progress increment.
        /// </summary>
        /// <value>
        /// The progress increment.
        /// </value>
        [XmlAttribute]
        public float ProgressIncrement { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerResearchTech"/> class.
        /// </summary>
        public PlayerResearchTech()
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

            Player player = playerContext.Player;
            float goalTechnologyId = playerContext.GoalTechnologyId.HasValue ? playerContext.GoalTechnologyId.Value : GoalTechnologyId;
            Technology goalTech = player.Technologies.FirstOrDefault(t => t.Data.Id == goalTechnologyId);
            if (goalTech == null)
            {
                context.AddLogMessage($"Goal Technology {goalTechnologyId} not found.");
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            if (goalTech.IsAquired)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            if (goalTech.IsAvailable)
            {
                goalTech.ProgressIncrement = ProgressIncrement;
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            int? techId = GetTechnologyIdFromGoal(player, goalTech.Data.Parents);
            if (techId.HasValue)
            {
                Technology techToResearch = player.Technologies.FirstOrDefault(t => t.Data.Id == techId);
                if (techToResearch == null)
                {
                    context.AddLogMessage($"Wrong Technology {techId} found.");
                    returnCode = BehaviorReturnCode.Failure;
                    return returnCode;
                }

                techToResearch.ProgressIncrement = ProgressIncrement;
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }

        private int? GetTechnologyIdFromGoal(Player player, IEnumerable<int> techIds)
        {
            HashSet<int> parents = new HashSet<int>();
            int? foundId = null;
            foreach (var parentId in techIds)
            {
                Technology parentTech = player.Technologies.FirstOrDefault(t => t.Data.Id == parentId);
                Debug.Assert(parentTech != null, $"Player Research Tech Behavior - tech not found {parentId}");

                if (parentTech.IsAvailable)
                {
                    foundId = parentTech.Data.Id;
                }
                else if (parentTech.IsNotAvailable)
                {
                    foreach (var techId in parentTech.Data.Parents)
                    {
                        parents.Add(techId);
                    }
                }
            }

            if (parents.Count > 0)
            {
                foundId = GetTechnologyIdFromGoal(player, parents);
            }

            return foundId;
        }
    }
}
