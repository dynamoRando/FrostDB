using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class DeleteStatement : IStatement
    {

        #region Private Fields
        #endregion

        #region Public Properties
        public string RawStatement { get; set; }
        public bool HasWhereClause => CheckIfHasWhereClause();
        public List<string> Tables { get; set; }
        public WhereClause WhereClause { get; set; }
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public DeleteStatement()
        {
            Tables = new List<string>();
            WhereClause = new WhereClause();
        }
        #endregion

        #region Public Methods
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
