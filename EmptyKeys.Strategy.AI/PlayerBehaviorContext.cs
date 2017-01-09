using System.Collections.Generic;
using System.Runtime.Serialization;
using EmptyKeys.Strategy.Core;
using EmptyKeys.Strategy.Diplomacy;
using EmptyKeys.Strategy.Environment;
using EmptyKeys.Strategy.Units;
using EmptyKeys.Strategy.Units.Upgrades;
using ProtoBuf;

namespace EmptyKeys.Strategy.AI
{
    /// <summary>
    /// Implements Player Behavior Context for players AI
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.BaseBehaviorContext" />
    [KnownType(typeof(Planet))]
    [KnownType(typeof(BaseUnit))]

    [ProtoContract(AsReferenceDefault = true)]
    public class PlayerBehaviorContext : BaseBehaviorContext
    {
        /// <summary>
        /// Player Behavior Context type registration
        /// </summary>
        public static readonly bool IsRegistered = BaseBehaviorContext.RegisterBehaviorContext(typeof(Player), creator);

        private static BaseBehaviorContext creator(object playerObject)
        {
            Player player = playerObject as Player;
            return new PlayerBehaviorContext { Player = player };
        }

        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        /// <value>
        /// The player.
        /// </value>
        [ProtoMember(1, AsReference = true)]
        [DataMember]
        public Player Player { get; set; }

        /// <summary>
        /// Gets or sets the behavior target.
        /// </summary>
        /// <value>
        /// The behavior target.
        /// </value>
        [ProtoMember(2, AsReference = true)]
        [DataMember]
        public HexElement BehaviorTarget { get; set; }

        /// <summary>
        /// Gets or sets the behavior.
        /// </summary>
        /// <value>
        /// The behavior.
        /// </value>
        [IgnoreDataMember]
        public Behavior Behavior { get; set; }

        /// <summary>
        /// Gets or sets the goal technology identifier.
        /// </summary>
        /// <value>
        /// The goal technology identifier.
        /// </value>
        [IgnoreDataMember]
        public int? GoalTechnologyId { get; set; }

        /// <summary>
        /// Gets or sets the unit.
        /// </summary>
        /// <value>
        /// The unit.
        /// </value>
        [IgnoreDataMember]
        public BaseUnit Unit { get; set; }

        /// <summary>
        /// Gets or sets the unit upgrade.
        /// </summary>
        /// <value>
        /// The unit upgrade.
        /// </value>
        [IgnoreDataMember]
        public BaseUnitUpgrade UnitUpgrade { get; set; }

        /// <summary>
        /// Gets the relation values.
        /// </summary>
        /// <value>
        /// The relation values.
        /// </value>
        [IgnoreDataMember]
        public IEnumerator<PlayerRelationValue> RelationValues { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerBehaviorContext"/> class.
        /// </summary>
        public PlayerBehaviorContext()
            : base()
        {
        }
    }
}
