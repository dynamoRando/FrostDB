using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class WhereClause
    {
        #region Private Fields
        #endregion

        #region Public Properties
        public List<StatementPart> Conditions { get; set; }
        public string WhereClauseText { get; set; }
        public string WhereClauseWithWhiteSpace { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public WhereClause()
        {
            WhereClauseText = string.Empty;
            WhereClauseWithWhiteSpace = string.Empty;
            Conditions = new List<StatementPart>();
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

    }
}
