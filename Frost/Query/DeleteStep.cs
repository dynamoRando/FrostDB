using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class DeleteStep : IPlanStep
    {
        #region Private Fields
        private Process _process;
        #endregion

        #region Public Properties
        public Guid Id { get; set; }
        public int Level { get; set; }
        public string TableName { get; set; }
        public string DatabaseName { get; set; }
        public IPlanStep InputStep { get; set; }
        public bool HasInputStep => CheckHasInputStep();
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public DeleteStep()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Public Methods
        public StepResult GetResult(Process process, string databaseName)
        {
            _process = process;
            var result = new StepResult();
            var resultRows = new List<Row>();

            // if we have an input step then we need to get the rows from the input step and then 
            // delete these particular rows
            if (HasInputStep)
            {
                var resultStep = InputStep.GetResult(_process, DatabaseName);
                foreach (var row in resultStep.Rows)
                {
                    var table = _process.GetDatabase(DatabaseName).GetTable(TableName);
                    foreach(var r in table.Rows)
                    {
                        table.RemoveRow(r.RowId);
                    }
                }
                resultRows = resultStep.Rows;
            }
            else
            {
                // delete all rows in the table
                var table = _process.GetDatabase(DatabaseName).GetTable(TableName);
                var rows = table.GetAllRows();
                table.RemoveAllRows();
                resultRows = rows;
            }

            result.Rows = resultRows;
            return result;
        }

        public string GetResultText()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        private bool CheckHasInputStep()
        {
            if (InputStep != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion


    }
}
