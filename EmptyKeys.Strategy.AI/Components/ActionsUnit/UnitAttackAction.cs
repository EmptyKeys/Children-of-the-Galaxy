using EmptyKeys.Strategy.Environment;
using EmptyKeys.Strategy.Units;
using EmptyKeys.Strategy.Units.Tasks;

namespace EmptyKeys.Strategy.AI.Components.ActionsUnit
{
    /// <summary>
    /// Implements unit attack action for behavior.
    /// </summary>
    /// <seealso cref="EmptyKeys.Strategy.AI.Components.BehaviorComponentBase" />
    public class UnitAttackAction : BehaviorComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitAttackAction"/> class.
        /// </summary>
        public UnitAttackAction()
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

            StaticUnit unit = unitContext.Unit as StaticUnit;
            if (unit == null || 
                unit.Environment is Galaxy || 
                unit.Target == null || 
                unit.Environment != unit.Target.Environment ||
                !unit.CanAttack ||
                unit.Owner == unit.Target.Owner)
            {
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }                                  

            context.AddLogMessage("Attacking Target - " + unit.Target.Name);
            AttackTask task = new AttackTask(unit);
            while (!task.IsTaskFinished)
            {
                task.Execute();
            }

            returnCode = BehaviorReturnCode.Success;
            return returnCode;
        }
    }
}
