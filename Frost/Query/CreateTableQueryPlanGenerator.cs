using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class CreateTableQueryPlanGenerator
    {
        #region Private Fields
        Process _process;
        #endregion

        #region Public Properties
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public CreateTableQueryPlanGenerator(Process process)
        {
            _process = process;
        }
        #endregion

        #region Public Methods
        public QueryPlan GeneratePlan(CreateTableStatement statement)
        {
            var step = new CreateTableStep();
            step.Columns.AddRange(statement.ColumnNamesAndTypes);

            var plan = new QueryPlan();
            plan.Steps.Add(step);

            return plan;
        }
        #endregion

        #region Private Methods
        #endregion

    }
}
