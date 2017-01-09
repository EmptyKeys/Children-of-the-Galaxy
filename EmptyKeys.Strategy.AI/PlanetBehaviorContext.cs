using System.Runtime.Serialization;
using EmptyKeys.Strategy.Environment;
using EmptyKeys.Strategy.Environment.Factory;
using ProtoBuf;

namespace EmptyKeys.Strategy.AI
{
    /// <summary>
    /// Implements Planet Behavior Context for planets AI
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.BaseBehaviorContext" />
    [ProtoContract(AsReferenceDefault = true)]
    public class PlanetBehaviorContext : BaseBehaviorContext
    {
        /// <summary>
        /// Planet Behavior Context type registration
        /// </summary>
        public static readonly bool IsRegistered = BaseBehaviorContext.RegisterBehaviorContext(typeof(Planet), creator);

        private static BaseBehaviorContext creator(object planetObject)
        {
            Planet planet = planetObject as Planet;
            return new PlanetBehaviorContext { Planet = planet };
        }

        /// <summary>
        /// Gets or sets the planet.
        /// </summary>
        /// <value>
        /// The planet.
        /// </value>
        [ProtoMember(1, AsReference = true)]
        [DataMember]
        public Planet Planet { get; set; }

        /// <summary>
        /// Gets or sets the item to build.
        /// </summary>
        /// <value>
        /// The item to build.
        /// </value>
        [ProtoMember(2, AsReference = true)]
        [DataMember]
        public FactoryItem ItemToBuild { get; set; }

        /// <summary>
        /// Gets or sets the name of the item behavior.
        /// </summary>
        /// <value>
        /// The name of the item behavior.
        /// </value>
        [ProtoMember(3)]
        [DataMember]
        public string ItemBehaviorName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlanetBehaviorContext"/> class.
        /// </summary>
        public PlanetBehaviorContext()
            : base()
        {     
        }
    }
}
