using System;
using System.Collections.Generic;
using System.Text;


public class SelectStatement : IStatement
{
    #region Public Properties
    public List<StatementPart> Statements { get; set; }
    public List<string> SelectList { get; set; }
    public string SelectListRaw { get; set; }
    public string RawStatement { get; set; }
    public string WhereClause { get; set; }
    public string WhereClauseWithWhiteSpace { get; set; }
    public List<string> Tables { get; set; }
    #endregion

    #region Constructors
    public SelectStatement()
    {
        Statements = new List<StatementPart>();
        SelectList = new List<string>();
        Tables = new List<string>();
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Properties
    #endregion
}
