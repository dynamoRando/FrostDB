using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrostDB
{
    public class TableStep : IPlanStep
    {
        #region Private Fields
        private SelectStatement _selectStatement;
        private Process _process;
        #endregion

        #region Public Properties
        public string TableName { get; set; }
        public List<string> Columns { get; set; }
        public Guid Id { get; set; }
        public int Level { get; set; }
        public bool IsValid { get; set; }
        #endregion

        #region Constructors
        public TableStep(SelectStatement statement)
        {
            Id = Guid.NewGuid();
            Columns = new List<string>();
            _selectStatement = statement;
            IsValid = true;
        }
        #endregion

        #region Public Methods
        public StepResult GetResult(Process process, string databaseName)
        {
            var result = new StepResult();
            _process = process;
            var tablename = _selectStatement.Tables.First() ?? string.Empty;
            
            if (_process.HasDatabase(databaseName))
            {
                var db = _process.GetDatabase(databaseName);
                if (db.HasTable(tablename))
                {
                    var table = db.GetTable(tablename);
                    foreach(var row in table.Rows)
                    {
                        var r = row.Get(_process);
                        result.Rows.Add(r);
                    }
                }
                else
                {
                    result.IsValid = false;
                    result.ErrorMessage = "Table Not Found";
                }
            }
            else
            {
                result.IsValid = false;
                result.ErrorMessage = "Database Not Found";
            }

            return result;
        }

        public string GetResultText()
        {
            var result = string.Empty;

            foreach(var column in Columns)
            {
                result += $"Searching column: {column}" + Environment.NewLine;
            }

            result += $"Searching table: {TableName}";

            return result;
        }
        #endregion
    }
}
