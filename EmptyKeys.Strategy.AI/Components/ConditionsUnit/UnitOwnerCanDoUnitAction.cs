using System.Xml.Serialization;
using EmptyKeys.Strategy.Units;

namespace EmptyKeys.Strategy.AI.Components.Conditions
{
    /// <summary>
    /// Implements unit condition for behavior. This condition is Success if unit can do unit Action.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class UnitOwnerCanDoUnitAction : BehaviorComponentBase
    {
        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        [XmlAttribute]
        public UnitActions Action { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOwnerCanDoUnitAction"/> class.
        /// </summary>
        public UnitOwnerCanDoUnitAction()
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

            if (unitContext.Unit.Owner.CanDoUnitAction(Action))
            {
                returnCode = BehaviorReturnCode.Success;
                return returnCode;
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
