using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using FrostDB;

namespace FrostDB
{
    /// <summary>
    /// Manages all disk related activites for FrostDb. DbStorage.cs is for all disk related actions for a specific db.
    /// </summary>
    public class StorageManager : IStorageManager
    {
        #region Private Fields
        private Process _process;
        private string _databaseFolder;
        private DbDirectory _directory;
        private Pager _pager;
        #endregion

        #region Public Properties
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public StorageManager() { }

        public StorageManager(Process process)
        {
            if (process != null)
            {
                _process = process;
                _databaseFolder = _process.Configuration.DatabaseFolder;
                _pager = new Pager(_process, _databaseFolder);
            }
            else
            {
                throw new ArgumentNullException(nameof(process));
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Creates the appropriate disk files for a new database hosted by this Frost process
        /// </summary>
        /// <param name="database"></param>
        public void AddNewDatabase(Database2 database)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a page from disk or cache for the specified parameters
        /// </summary>
        /// <param name="databaseName">The database name</param>
        /// <param name="tableName">The table name</param>
        /// <param name="pageId">The page Id</param>
        /// <returns>A page from cache or disk</returns>
        public Page GetPage(string databaseName, string tableName, int pageId)
        {
            return _pager.GetPage(databaseName, tableName, pageId);
        }

        /// <summary>
        /// Returns a page from disk or cache for the specified parameter
        /// </summary>
        /// <param name="databaseId">The datababse id</param>
        /// <param name="tableId">The table id</param>
        /// <param name="pageId">The page id</param>
        /// <returns>A page from cache or disk</returns>
        public Page GetPage(int databaseId, int tableId, int pageId)
        {
            return _pager.GetPage(databaseId, tableId, pageId);
        }

        /// <summary>
        /// Gets a list of databases from disk
        /// </summary>
        /// <returns>A list of databases</returns>
        public List<Database2> GetDatabases()
        {
            var result = new List<Database2>();
            var databases = GetListOfOnlineDatabases();
            foreach(var db in databases)
            {
                var storage = new DbStorage(_process);
                var dbItem = storage.GetDatabase(db);
                result.Add(dbItem);
            }

            return result;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Returns the list of databases that this Process hosts that are online
        /// </summary>
        /// <returns>A string list of online databases</returns>
        private List<string> GetListOfOnlineDatabases()
        {
            var result = new List<string>();
            var file = Path.Combine(_databaseFolder, _process.Configuration.DatabaseDirectoryFileName);
            if (File.Exists(file))
            {
                var items = File.ReadAllLines(file).ToList();
                _directory = new DbDirectory(items);
            }
            else
            {
                File.Create(file);
            }

            result.AddRange(_directory.OnlineDatabases);

            return result;
        }

        private SchemaFile GetSchemaFile(string databaseName)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
