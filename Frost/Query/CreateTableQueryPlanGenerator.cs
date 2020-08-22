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
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        #endregion

    }
}
