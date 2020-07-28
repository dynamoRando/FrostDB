﻿using System;
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
