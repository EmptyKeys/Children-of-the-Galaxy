using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace EmptyKeys.Strategy.AI.Components.Conditions
{
    public class Conditional : BehaviorComponentBase
    {
        private Func<Boolean> conditionFunction;

        /// <summary>
        /// Returns a return code equivalent to the test 
        /// -Returns Success if true
        /// -Returns Failure if false
        /// </summary>
        /// <param name="test">the value to be tested</param>
        public Conditional(Func<Boolean> test)
        {
            this.conditionFunction = test;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorReturnCode Behave(IBehaviorContext context)
        {
            try
            {
                switch (conditionFunction.Invoke())
                {
                    case true:
                        returnCode = BehaviorReturnCode.Success;
                        return returnCode;
                    case false:
                        returnCode = BehaviorReturnCode.Failure;
                        return returnCode;
                    default:
                        returnCode = BehaviorReturnCode.Failure;
                        return returnCode;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                returnCode = BehaviorReturnCode.Failure;
                return returnCode;
            }
        }
    }
}
