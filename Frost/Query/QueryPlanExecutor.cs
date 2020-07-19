using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;

namespace QueryParserConsole.Query
{
    public class QueryPlanExecutor
    {
        #region Private Fields
        #endregion

        #region Public Properties
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        public void Execute(QueryPlan plan)
        {
            plan.Steps.OrderBy(s => s.Level);

            Console.WriteLine("Executing Plan...");

            foreach (var step in plan.Steps)
            {
                Console.WriteLine("Executing Step...");
                Console.WriteLine($"Step Level: {step.Level.ToString()}");
                step.GetResultText();
            }
        }
        #endregion

        #region Private Methods
        #endregion

    }
}
