using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FrostDB.Interface;

namespace FrostDB.Processing
{
    /// <summary>
    /// Responsible for managing the collection of databases for this FrostDb instance
    /// </summary>
    class DatabaseManager2
    {
        #region Private Fields
        private string _databaseFolder;
        private Database2[] _databases;
        private StorageManager _storageManager;
        #endregion

        #region Public Properties
        public ReadOnlySpan<Database2> Databases => GetDatabases();
        #endregion

        #region Constructors
        public DatabaseManager2(string databaseFolder, StorageManager storageManager)
        {
            _databaseFolder = databaseFolder;
            _storageManager = storageManager;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Loads all databases from disk (used in Frost process startup)
        /// </summary>
        /// <returns>A list of databases</returns>
        public int LoadDatabases()
        {
            int count = 0;
            
            if (!Directory.Exists(_databaseFolder))
            {
                Directory.CreateDirectory(_databaseFolder);
            }

            var dbs = _storageManager.GetDatabases();
            _databases = dbs;
            count = dbs.Length;

            return count;
        }
        #endregion

        #region Private Methods
        private ReadOnlySpan<Database2> GetDatabases()
        {
            return new ReadOnlySpan<Database2>(_databases);
        }
        #endregion

    }
}
