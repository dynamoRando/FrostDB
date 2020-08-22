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
                var table = new Table(process);
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
        #endregion

    }
}
