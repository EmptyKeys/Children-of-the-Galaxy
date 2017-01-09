using System.Linq;
using System.Xml.Serialization;
using EmptyKeys.Strategy.Environment;

namespace EmptyKeys.Strategy.AI.Components.ActionsPlayer
{
    /// <summary>
    /// Implements player action for behavior. This action finds avail planet for building with highest Production.
    /// The result is stored in BehaviorTarget of PlayerBehaviorContext.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class FindPlanetForBuild : BehaviorComponentBase
    {
        /// <summary>
        /// Gets or sets the name of the ignore behavior.
        /// </summary>
        /// <value>
        /// The name of the ignore behavior.
        /// </value>
        [XmlAttribute]
        public string IgnoreBehaviorName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [planet with trade route].
        /// </summary>
        /// <value>
        /// <c>true</c> if [planet with trade route]; otherwise, <c>false</c>.
        /// </value>
        [XmlAttribute]
        public bool PlanetWithTradeRoute { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FindPlanetForBuild"/> class.
        /// </summary>
        public FindPlanetForBuild()
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

            float maxProduction = float.MinValue;
            Planet bestPlanet = null;
            foreach (var body in playerContext.Player.StarSystemBodies)
            {
                Planet planet = body as Planet;
                if (planet == null)
                {
                    continue;
                }

                if (maxProduction > planet.AvailProduction || 
                    (!string.IsNullOrEmpty(IgnoreBehaviorName) && planet.Behavior != null && planet.Behavior.Name == IgnoreBehaviorName))
                {
                    continue;
                }

                int activeTradeRoutesCount = planet.Owner.TradeRoutes.Count(tr => tr.From == planet);
                if (PlanetWithTradeRoute && (activeTradeRoutesCount - planet.TradeRoutesCount) == 0)
                {
                    continue;
                }

                maxProduction = planet.AvailProduction;
                bestPlanet = planet;
            }

            if (bestPlanet != null)
            {
                playerContext.BehaviorTarget = bestPlanet;
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
