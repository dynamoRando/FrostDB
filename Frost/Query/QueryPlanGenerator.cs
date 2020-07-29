using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
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
        public QueryPlan GeneratePlan(IStatement statement, string databaseName)
        {
            var plan = GeneratePlan(statement);
            plan.DatabaseName = databaseName;
            return plan;
        }

        public QueryPlan GeneratePlan(IStatement statement)
        {
            var plan = new QueryPlan();
            if (statement is SelectStatement)
            {
                plan = new SelectQueryPlanGenerator().GeneratePlan((statement as SelectStatement));
            }

            if (statement is InsertStatement)
            {
                var insert = statement as InsertStatement;
                throw new NotImplementedException();
            }

            return plan;
        }
        #endregion

        #region Private Methods
        #endregion
    }
}