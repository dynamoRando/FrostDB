using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class TableStep : IPlanStep
    {
        public string TableName { get; set; }
        public List<string> Columns { get; set; }
        public Guid Id { get; set; }
        public int Level { get; set; }

        public TableStep()
        {
            Id = Guid.NewGuid();
            Columns = new List<string>();
        }

        public PlanResult GetResult()
        {
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
    }
}
