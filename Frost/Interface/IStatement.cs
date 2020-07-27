using System;
using System.Collections.Generic;
using System.Text;

public interface IStatement
{
    List<string> Tables { get; set; }
    List<StatementPart> Statements { get; set; }
    string WhereClauseWithWhiteSpace { get; set; }
    string WhereClause { get; set; }
    string RawStatement { get; set; }
}
