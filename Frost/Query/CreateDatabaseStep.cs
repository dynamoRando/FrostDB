using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FrostDB
{
    class CreateDatabaseStep : IPlanStep
    {
        #region Private Fields
        #endregion

        #region Public Properties
        public Guid Id { get; set; }
        public int Level { get; set; }
        public bool IsValid { get; set; }
        public string DatabaseName { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        public StepResult GetResult(Process process, string databaseName)
        {
            var result = new StepResult();
            process.AddDatabase2(databaseName);
            result.IsValid = true;
            result.RowsAffected = 0;
            result.Rows = new List<Row>();
            return result;
        }

        public string GetResultText()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        #endregion


    }
}
