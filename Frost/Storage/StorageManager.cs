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
        /// Attempts to insert a row into a btree
        /// </summary>
        /// <param name="insert">The row to insert</param>
        /// <returns>True if successful, otherwise false</returns>
        public bool InsertRow(RowInsert insert)
        {
            return _pager.InsertRow(insert);
        }

        /// <summary>
        /// Creates the appropriate disk files for a new database hosted by this Frost process
        /// </summary>
        /// <param name="database"></param>
        public void AddNewDatabase(Database2 database)
        {
            database.Storage.CreateFiles();
            _directory.AddDatabaseToDirectory(database, true);
        }

        public List<Row2> GetAllRows(BTreeAddress treeAddress)
        {
            return _pager.GetAllRows(treeAddress);
        }

        /// <summary>
        /// Gets a list of databases from disk
        /// </summary>
        /// <returns>A list of databases</returns>
        public Database2[] GetDatabases()
        {
            var databases = GetOnlineDatabases();
            Database2[] result = new Database2[databases.Length];
            int i = 0;

            foreach (var db in databases)
            {
                var storage = new DbStorage(_process, db);
                var dbItem = storage.GetDatabase(db);
                result[i] = dbItem;
                i++;
            }

            return result;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Returns the list of databases that this Process hosts that are online
        /// </summary>
        /// <returns>A string list of online databases</returns>
        private string[] GetOnlineDatabases()
        {
            var file = Path.Combine(_databaseFolder, _process.Configuration.DatabaseDirectoryFileName);
            if (File.Exists(file))
            {
                var items = File.ReadAllLines(file);
                _directory = new DbDirectory(items);
            }
            else
            {
                using (var fs = File.Create(file))
                {
                    fs.Flush();
                }

                var items = File.ReadAllLines(file);
                _directory = new DbDirectory(items);
            }

            return _directory.OnlineDatabases;
        }

        private SchemaFile GetSchemaFile(string databaseName)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
