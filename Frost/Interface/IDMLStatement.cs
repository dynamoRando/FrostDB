using FrostDB;
using System;
using System.Collections.Generic;
using System.Text;

public interface IDMLStatement
{
    List<string> Tables { get; set; }
    bool HasWhereClause { get; }
    WhereClause WhereClause { get; set; }
    string RawStatement { get; set; }
    bool IsValid { get; set; }
    string ErrorMessage { get; set; }
    public string DatabaseName { get; set; }
}
