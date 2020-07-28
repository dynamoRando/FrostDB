using FrostDB;
using System;
using System.Collections.Generic;
using System.Text;

public interface IStatement
{
    List<string> Tables { get; set; }
    bool HasWhereClause { get; }
    WhereClause WhereClause { get; set; }
    string RawStatement { get; set; }
}
