using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Query
{
    public class ColumnOutputStep : IPlanStep
    {

        #region Private Fields
        #endregion

        #region Public Properties
        public Guid Id { get; set; }
        public int Level { get; set; }
        public bool IsValid { get; set; }
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
            throw new NotImplementedException();
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
