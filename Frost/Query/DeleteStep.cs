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
        public bool ShouldDeleteAllRows { get; set; }
        public List<Row> DeletedRows { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public DeleteStep()
        {
            Id = Guid.NewGuid();
            ShouldDeleteAllRows = false;
            DeletedRows = new List<Row>();
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
                var table = _process.GetDatabase(DatabaseName).GetTable(TableName);
                foreach (var row in resultStep.Rows)
                {
                    table.RemoveRow(row.Id);
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
            var result = string.Empty;

            result += $"For database {DatabaseName} for table {TableName}" + Environment.NewLine;
            if (HasInputStep)
            {
                result += $"Delete Rows In Where Clause" + Environment.NewLine;
            }
            else
            {
                result += $"Delete All Rows In Table" + Environment.NewLine;
            }

            return result;
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
