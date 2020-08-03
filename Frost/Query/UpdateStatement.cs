using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class UpdateStatement : IStatement
    {

        #region Private Fields
        #endregion

        #region Public Properties
        public List<string> Tables { get; set; }
        public bool HasWhereClause => CheckIfHasWhereClause();
        public string RawStatement { get; set; }
        public WhereClause WhereClause { get; set; }
        public List<UpdateStatementElement> Elements { get; set; }
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public UpdateStatement()
        {
            Tables = new List<string>();
            WhereClause = new WhereClause();
            Elements = new List<UpdateStatementElement>();
            ErrorMessage = string.Empty;
        }
        #endregion

        #region Public Methods
        public void ParseElements()
        {
            foreach(var element in Elements)
            {
                var items = element.RawStringWithWhitespace.Split('=');
                if (items.Length == 2)
                {
                    element.ColumnName = items[0].Trim();
                    element.Operator = "=";
                    element.Value = items[1].Trim().Replace("'", string.Empty);
                }
            }
        }
        #endregion

        #region Private Methods
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
}
