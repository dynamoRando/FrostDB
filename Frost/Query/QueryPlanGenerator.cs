using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrostDB
{
    public class QueryPlanGenerator
    {
        #region Private Fields
        Process _process;
        #endregion

        #region Public Properties
        #endregion

        #region Constructors
        public QueryPlanGenerator(Process process)
        {
            _process = process;
        }
        #endregion

        #region Public Methods
        public QueryPlan GeneratePlan(FrostIDDLStatement statement, string databaseName)
        {
            throw new NotImplementedException();
        }
        public QueryPlan GeneratePlan(FrostIDMLStatement statement, string databaseName)
        {
            var plan = GeneratePlan(statement);
            plan.DatabaseName = databaseName;
            return plan;
        }

        public QueryPlan GeneratePlan(FrostIDMLStatement statement)
        {
            var plan = new QueryPlan();
            if (statement is SelectStatement)
            {
                plan = new SelectQueryPlanGenerator(_process).GeneratePlan((statement as SelectStatement));
            }

            if (statement is InsertStatement)
            {
                plan = new InsertQueryPlanGenerator(_process).GeneratePlan((statement as InsertStatement));
            }

            if (statement is UpdateStatement)
            {
                plan = new UpdateQueryPlanGenerator(_process).GeneratePlan((statement as UpdateStatement));
            }

            if (statement is DeleteStatement)
            {
                plan = new DeleteQueryPlanGenerator(_process).GeneratePlan((statement as DeleteStatement));
            }

            return plan;
        }
        #endregion

        #region Private Methods
        #endregion
    }
}