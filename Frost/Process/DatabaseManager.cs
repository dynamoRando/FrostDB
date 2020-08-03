using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace FrostDB
{
    public class DatabaseManager 
    {
        #region Private Fields
        private List<Database> _databases;
        private string _databaseFolder;
        private string _databaseExtension;
        private IDataFileManager<DataFile> _dataFileManager;
        private IDatabaseFileMapper<Database, DataFile> _databaseFileMapper;
        private IDataManagerEventManager _dataEventManager;
        private Process _process;
        #endregion

        #region Public Properties
        public List<Database> Databases => _databases;
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
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

        }
        #endregion

        #region Public Methods
        public Database GetFullDatabase(string databaseName)
        {
            Database db = null;

            Databases.ForEach(database =>
            {
                if ((database is Database) && (database.Name == databaseName))
                {
                    db = database as Database;
                }
            });

            return db;
        }
        public void AddDatabase(Database database)
        {
            if (!HasDatabase(database.Name))
            {
                _databases.Add(database);
                SaveToDisk(database);
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

        public bool HasDatabase(Guid guid)
        {
            return Databases.Any(d => d.Id == guid);
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
