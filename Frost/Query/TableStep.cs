using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class TableStep : IPlanStep
    {
        #region Private Fields
        private Process _process;
        #endregion

        #region Public Properties
        public string TableName { get; set; }
        public List<string> Columns { get; set; }
        public Guid Id { get; set; }
        public int Level { get; set; }
        #endregion

        #region Constructors
        public TableStep()
        {
            Id = Guid.NewGuid();
            Columns = new List<string>();
        }
        #endregion

        #region Public Methods
        public StepResult GetResult(Process process, string databaseName)
        {
            _process = process;
            throw new NotImplementedException();
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
