using System;
using System.Collections.Generic;
using System.Text;

namespace QueryParserConsole.Query
{
    public class QueryPlanGenerator
    {
        #region Private Fields
        #endregion

        #region Public Properties
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        public QueryPlan GeneratePlan(IStatement statement)
        {
            var plan = new QueryPlan();
            if (statement is SelectStatement)
            {
                plan = new SelectQueryPlanGenerator().GeneratePlan((statement as SelectStatement));
            }

            return plan;
        }
        #endregion

        #region Private Methods
        #endregion
    }
}
