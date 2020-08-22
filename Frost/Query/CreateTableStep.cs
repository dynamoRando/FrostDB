using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class CreateTableStep : IPlanStep
    {
        #region Private Fields
        #endregion

        #region Public Properties
        public List<string> Columns { get; set; }
        public Guid Id { get; set; }
        public int Level { get; set; }
        public bool IsValid { get; set; }
        public string TableName { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public CreateTableStep()
        {
            Columns = new List<string>();
            IsValid = true;
            Level = 1;
            Id = Guid.NewGuid();
        }

        public StepResult GetResult(Process process, string databaseName)
        {
            var result = new StepResult();
            if (!process.HasDatabase(databaseName))
            {
                var db = process.GetDatabase(databaseName);
                var columns = new List<ColumnSchema>();

                foreach(var column in Columns)
                {
                    columns.Add(GetColumnSchema(column));
                }

                var schema = new TableSchema2(columns, TableName, databaseName);
                var table = new Table2(process, schema);
                db.AddTable(table);
                throw new NotImplementedException();
            }
            else
            {
                result.IsValid = false;
                result.ErrorMessage = $"{databaseName} was not found";
            }

            return result;
        }

        public string GetResultText()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        private ColumnSchema GetColumnSchema(string text)
        {
            var result = new ColumnSchema();
            var values = text.Split(" ");
            if (values.Length == 3)
            {
                result.Name = values[0];
                result.DataType = values[1];
                if (values[2] == "NULL")
                {
                    result.IsNullable = true;
                }
                else if (values[2] == "NOT NULL")
                {
                    result.IsNullable = false;
                }
            }
            else if (values.Length == 2)
            {
                result.Name = values[0];
                result.DataType = values[1];
                result.IsNullable = true;
            }
            return result;
        }
        #endregion

    }
}
