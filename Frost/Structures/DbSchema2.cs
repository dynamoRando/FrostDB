using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    /// <summary>
    /// Represents the schema of a database
    /// </summary>
    public class DbSchema2
    {
        #region Private Fields
        #endregion

        #region Public Properties
        public string DatabaseName { get; set; }
        public int DatabaseId { get; set; }
        public List<TableSchema2> Tables { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public DbSchema2(int id, string databaseName)
        {
            DatabaseId = id;
            DatabaseName = databaseName;
        }

        public DbSchema2()
        {
            Tables = new List<TableSchema2>();
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion


    }
}
