using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Query
{
    public class ColumnOutputStep : IPlanStep
    {
        public Guid Id { get; set; }
        public int Level { get; set; }

        public StepResult GetResult(Process process, string databaseName)
        {
            throw new NotImplementedException();
        }

        public string GetResultText()
        {
            throw new NotImplementedException();
        }
    }
}
