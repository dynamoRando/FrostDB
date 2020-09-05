using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using MoreLinq;
using System.Diagnostics.CodeAnalysis;

namespace FrostDB
{
    public class DatabaseManager 
    {
        #region Private Fields
        private List<Database> _databases;
        private List<Database2> _databases2;
        private string _databaseFolder;
        private string _databaseExtension;
        private IDataFileManager<DataFile> _dataFileManager;
        private IDatabaseFileMapper<Database, DataFile> _databaseFileMapper;
        private IDataManagerEventManager _dataEventManager;
        private Process _process;
        private StorageManager _storageManager;
        #endregion

        #region Public Properties
        public List<Database> Databases => _databases;
        public List<Database2> Databases2 => _databases2;
        public StorageManager StorageManager => _storageManager;
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        // new constructor
        public DatabaseManager(string databaseFolder, Process process)
        {
            _process = process;
            _databaseFolder = databaseFolder;
            _storageManager = new StorageManager(_process);
            _databases2 = new List<Database2>();
        }

        public DatabaseManager(string databaseFolder,
            string databaseExtension,
            IDatabaseFileMapper<Database, DataFile> mapper,
            IDataManagerEventManager dataEventManager, Process process)
        {
            _dataFileManager = new DataFileManager();
            _databaseFileMapper = mapper;

            _databaseFolder = databaseFolder;
            _databaseExtension = databaseExtension;

            if (_databaseFileMapper is null)
            {
                _databaseFileMapper = new DatabaseFileMapper();
            }

            _databases = new List<Database>();
            _process = process;

            // new
            _storageManager = new StorageManager(_process);
            _databases2 = new List<Database2>();

        }
        #endregion

        #region Public Methods
        public bool HasDatabase2(string databaseName)
        {
            return Databases2.Any(d => d.Name.ToUpper() == databaseName.ToUpper());
        }

        public bool HasDatabase2(int databaseId)
        {
            return Databases2.Any(d => d.DatabaseId == databaseId);
        }

        public Database2 GetDatabase2(string databaseName)
        {
            return Databases2.Where(d => d.Name == databaseName).FirstOrDefault();
        }

        public Database2 GetDatabase2(int databaseId)
        {
            return Databases2.Where(d => d.DatabaseId == databaseId).FirstOrDefault();
        }

        public int GetNextDatabaseId()
        {
            return GetMaxDatabaseId() + 1;
        }

        public int GetMaxDatabaseId()
        {
            return Databases2.MaxBy(d => d.DatabaseId).First().DatabaseId;
        }

        public void AddDatabase(Database database)
        {
            if (!HasDatabase(database.Name))
            {
                _databases.Add(database);
                SaveToDisk(database);
            }
        }

        public void AddDatabase2(Database2 database)
        {
            if (database != null)
            {
                if (!HasDatabase2(database.Name))
                {
                    _databases2.Add(database);
                    _storageManager.AddNewDatabase(database);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(database));
            }
        }

        public Database GetDatabase(string databaseName)
        {
            return Databases.Where(d => d.Name.ToUpper() == databaseName.ToUpper()).FirstOrDefault();
        }

        public Database GetDatabase(Guid? guid)
        {
            return Databases.Where(d => d.Id == guid).FirstOrDefault();
        }

        public bool HasDatabase(string databaseName)
        {
            return Databases.Any(d => d.Name == databaseName);
        }

        public void RemoveDatabase(Guid guid)
        {
            throw new NotImplementedException();
        }

        public void RemoveDatabase(string databaseName)
        {
            File.Delete(_databaseFolder + @"\" + databaseName + _databaseExtension);
            var db = (Database)_process.GetDatabase(databaseName);
            _databases.Remove(db);
        }

        /// <summary>
        /// Loads all databases from disk (used in Frost process startup)
        /// </summary>
        /// <returns>A list of databases</returns>
        public int LoadDatabases2()
        {
            int count = 0;
            string databaseFolderLocation = _process.Configuration.DatabaseFolder;

            if (!Directory.Exists(databaseFolderLocation))
            {
                Directory.CreateDirectory(databaseFolderLocation);
            }

            var dbs = _storageManager.GetDatabases();
            _databases2.AddRange(dbs);
            count = dbs.Count;

            return count;
        }

        public int LoadDatabases(string databaseFolderLocation, string databaseExtension)
        {
            int count = 0;

            if (!Directory.Exists(databaseFolderLocation))
            {
                Directory.CreateDirectory(databaseFolderLocation);
            }

            foreach (var file in Directory.GetFiles(databaseFolderLocation, "*" + databaseExtension))
            {
                var database = GetDatabaseFromDisk(file);
                _databases.Add(database);
                count = Databases.Count;
            }

            return count;
        }

        public void SaveToDisk(Database database)
        {  
            var fileName =  Path.Combine(_databaseFolder, database.Name + _databaseExtension);
            var file = _databaseFileMapper.Map(database);
            _dataFileManager.SaveDataFile(fileName, file);
            _process.Log.Debug($"{database.Name} saved to disk at {fileName}");
        }
        #endregion

        #region Private Methods
        private Database GetDatabaseFromDisk(string file)
        {
            var dataFile = _dataFileManager.GetDataFile(file);

            return _databaseFileMapper.Map(dataFile, _process);
        }
        #endregion

    }
}
