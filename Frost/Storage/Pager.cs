using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    /// <summary>
    /// Responsible for returning pages from disk
    /// </summary>
    public class Pager
    {
        #region Private Fields
        private Process _process;
        private string _databaseFolder;
        #endregion

        #region Public Properties
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Pager(Process process, string databaseFolder)
        {
            _process = process;
            _databaseFolder = databaseFolder;
        }
        #endregion

        #region Public Methods
        public Page GetPage(string databaseName, string tableName, int pageId)
        {
            throw new NotImplementedException();
        }

        public Page GetPage(int databaseId, int tableId, int pageId)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        #endregion

    }
}
