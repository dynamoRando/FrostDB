using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    /// <summary>
    /// Used to pass row values to be added to a table.
    /// </summary>
    public class RowForm2
    {
        #region Private Fields
        #endregion

        #region Public Properties
        public string DatabaseName { get; set; }
        public string TableName { get; set; }
        public List<RowValue2> Values { get; set; }
        public Participant2 Participant { get; set; }
        public bool IsLocal { get; set; }
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

    }
}
