using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class UpdateStatement : IStatement
    {
        public List<string> Tables { get; set; }
        public List<StatementPart> Statements { get; set; }
        public string WhereClauseWithWhiteSpace { get; set; }
        public string WhereClause { get; set; }
        public string RawStatement { get; set; }

        public UpdateStatement()
        {
            Tables = new List<string>();
            Statements = new List<StatementPart>();
        }
    }
}
