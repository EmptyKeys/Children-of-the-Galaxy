using EmptyKeys.Strategy.Environment;

namespace EmptyKeys.Strategy.AI.Components.ConditionsUnit
{
    /// <summary>
    /// Implements unit condition for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class UnitIsEnemyInfluenceHigher : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitIsEnemyInfluenceHigher"/> class.
        /// </summary>
        public UnitIsEnemyInfluenceHigher()
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

            if (unitContext.Unit.Environment is Galaxy)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }

            float unitOwnerInfluence = 0;
            unitContext.Unit.Environment.PlayersInfluence.TryGetValue(unitContext.Unit.Owner, out unitOwnerInfluence);
            foreach (var playerInfluence in unitContext.Unit.Environment.PlayersInfluence)
            {
                if (playerInfluence.Key == unitContext.Unit.Owner)
                {
                    continue;
                }

                if (playerInfluence.Value > unitOwnerInfluence)
                {
                    returnCode = BehaviorReturnCode.Success;
                    return returnCode;
                }
            }

            returnCode = BehaviorReturnCode.Failure;
            return returnCode;
        }
    }
}
