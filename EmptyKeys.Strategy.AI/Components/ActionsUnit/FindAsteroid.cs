using System.Linq;
using System.Xml.Serialization;
using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Environment;

namespace EmptyKeys.Strategy.AI.Components.ActionsUnit
{
    /// <summary>
    /// Implements unit action for behavior. This action finds Asteroid with highest influence in the unit environment.
    /// The result is stored in EnvironmentTarget of UnitBehaviorContext.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class FindAsteroid : BehaviorComponentBase
    {
        /// <summary>
        /// Gets or sets a value indicating whether [enable unscanned check].
        /// </summary>
        /// <value>
        /// <c>true</c> if [enable unscanned check]; otherwise, <c>false</c>.
        /// </value>
        [XmlAttribute]
        public bool EnableUnscannedCheck { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [enable is extracted check].
        /// </summary>
        /// <value>
        /// <c>true</c> if [enable is extracted check]; otherwise, <c>false</c>.
        /// </value>
        [XmlAttribute]
        public bool EnableIsExtractedCheck { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FindAsteroid"/> class.
        /// </summary>
        public FindAsteroid()
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
            UnitBehaviorContext unitContext = context as UnitBehaviorContext;
            if (unitContext == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            BaseEnvironment envi = unitContext.Unit.Environment;
            if (envi == null)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            unitContext.EnvironmentTarget = null;
            float maxInfluence = float.MinValue;
            Asteroid bestAsteroid = null;

            foreach (var body in envi.EnvironmentMap.Values)
            {
                Asteroid asteroid = body as Asteroid;
                if (asteroid == null)
                {
                    continue;
                }

                if (EnableUnscannedCheck && unitContext.Unit.Owner.ScannedStarSystemBodies.Contains(asteroid.GlobalKey))
                {
                    continue;
                }

                if (EnableIsExtractedCheck && asteroid.IsExtracted)
                {
                    continue;
                }

                HexElement infElem;
                if (!envi.EnvironmentInfluenceMap.TryGetValue(asteroid.HexMapKey, out infElem))
                {
                    continue;
                }

                float asteroidInfluence = 0;
                InfluenceElement influence = infElem as InfluenceElement;
                if (influence != null)
                {
                    asteroidInfluence = influence.Value;
                }
                else
                {
                    MultiLayerElement layer = infElem as MultiLayerElement;
                    if (layer == null)
                    {
                        continue;
                    }

                    asteroidInfluence = layer.Values.Sum(i => ((InfluenceElement)i).Value);
                }

                if (asteroidInfluence > maxInfluence)
                {
                    maxInfluence = asteroidInfluence;
                    bestAsteroid = asteroid;
                }
            }

            unitContext.EnvironmentTarget = bestAsteroid;
            if (bestAsteroid != null)
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
