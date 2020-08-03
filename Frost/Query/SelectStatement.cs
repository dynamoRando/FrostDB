using FrostDB;
using System;
using System.Collections.Generic;
using System.Text;


public class SelectStatement : IStatement
{
    #region Public Properties
    public List<string> SelectList { get; set; }
    public string SelectListRaw { get; set; }
    public string RawStatement { get; set; }
    public List<string> Tables { get; set; }
    public WhereClause WhereClause { get; set; }
    public bool HasWhereClause => CheckIfHasWhereClause();
    public bool IsValid {get; set;}
    public string ErrorMessage {get; set;}
    #endregion

    #region Constructors
    public SelectStatement()
    {
        SelectList = new List<string>();
        Tables = new List<string>();
        WhereClause = new WhereClause();
        ErrorMessage = string.Empty;
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Properties
    private bool CheckIfHasWhereClause()
    {
        if (WhereClause.WhereClauseWithWhiteSpace.Length > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion
}
