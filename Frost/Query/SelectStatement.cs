using FrostDB;
using System;
using System.Collections.Generic;
using System.Text;


public class SelectStatement : FrostIDMLStatement
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
    public string DatabaseName { get; set; }
    #endregion

    #region Constructors
    public SelectStatement()
    {
        SelectList = new List<string>();
        Tables = new List<string>();
        WhereClause = new WhereClause();
        ErrorMessage = string.Empty;
        IsValid = true;
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Properties
    private bool CheckIfHasWhereClause()
    {
        if (WhereClause.TextWithWhiteSpace.Length > 0)
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
