using System.Runtime.Serialization;
using EmptyKeys.Strategy.Units;
using ProtoBuf;

namespace EmptyKeys.Strategy.AI
{
    /// <summary>
    /// Implements Unit Behavior Context for units AI
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.BaseBehaviorContext" />
    [ProtoContract(AsReferenceDefault = true)]
    public class UnitBehaviorContext : BaseBehaviorContext
    {
        /// <summary>
        /// Unit Behavior Context type registration
        /// </summary>
        public static readonly bool IsRegistered = BaseBehaviorContext.RegisterBehaviorContext(typeof(BaseUnit), creator);

        private static BaseBehaviorContext creator(object unit)
        {
            BaseUnit baseUnit = unit as BaseUnit;
            return new UnitBehaviorContext { Unit = baseUnit };
        }

        /// <summary>
        /// Gets or sets the unit.
        /// </summary>
        /// <value>
        /// The unit.
        /// </value>
        [ProtoMember(1, AsReference = true)]
        [DataMember]
        public BaseUnit Unit { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="UnitBehaviorContext"/> class.
        /// </summary>
        public UnitBehaviorContext()
            : base()
        {
        }
    }
}
