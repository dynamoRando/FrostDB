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
        public string Text { get; set; }
        public string TextWithWhiteSpace { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public WhereClause()
        {
            Text = string.Empty;
            TextWithWhiteSpace = string.Empty;
            Conditions = new List<StatementPart>();
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

    }
}
