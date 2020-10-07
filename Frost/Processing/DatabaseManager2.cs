using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FrostDB.Interface;
using System.Linq;

namespace FrostDB.Processing
{
    /// <summary>
    /// Responsible for managing the collection of databases for this FrostDb instance
    /// </summary>
    class DatabaseManager2
    {
        #region Private Fields
        private string _databaseFolder;
        private List<Database2> _databases;
        private List<string> _databaseNames;
        private StorageManager _storageManager;
        #endregion

        #region Public Properties
        public List<Database2> Databases => GetDatabases();
        public List<string> DatabaseNames => _databaseNames;
        #endregion

        #region Constructors
        public DatabaseManager2(string databaseFolder, StorageManager storageManager)
        {
            _databases = new List<Database2>();
            _databaseFolder = databaseFolder;
            _storageManager = storageManager;
            LoadDatabases();
        }
        #endregion

        #region Public Methods
        public void AddDatabase(Database2 database)
        {
            if (database != null)
            {
                if (!HasDatabase(database.Name))
                {
                    _databases.Add(database);
                    _storageManager.AddNewDatabase(database);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(database));
            }
        }

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
            _databases = new List<Database2>(dbs.Length);
            _databases.AddRange(dbs);
            count = dbs.Length;

            _databaseNames = new List<string>(dbs.Length);

            foreach(var db in _databases)
            {
                _databaseNames.Add(db.Name);
            }

            return count;
        }

        public Database2 GetDatabase(string databaseName)
        {
            return _databases.Where(d => d.Name.ToUpper() == databaseName.ToUpper()).First();
        }

        public bool HasDatabase(string databaseName)
        {
            return _databases.Any(d => d.Name.ToUpper() == databaseName.ToUpper()); ; 
        }

        public Database2 GetDatabase(int id)
        {
            return _databases.Where(d => d.DatabaseId == id).FirstOrDefault();
        }
        #endregion

        #region Private Methods
        private List<Database2> GetDatabases()
        {
            return _databases;
        }
        #endregion

    }
}
