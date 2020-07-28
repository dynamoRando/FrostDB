using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class InsertStatement : IStatement
    {
        public List<string> Tables { get; set; }
        public List<StatementPart> Statements { get; set; }
        public string RawStatement { get; set; }
        public string WhereClause { get; set; }
        public string WhereClauseWithWhiteSpace { get; set; }

        public List<string> ColumnNames { get; set; }
        public List<string> InsertValues {get; set;}

        public InsertStatement()
        {
            Tables = new List<string>();
            Statements = new List<StatementPart>();
            ColumnNames = new List<string>();
            InsertValues = new List<string>();
        }
    }
}
