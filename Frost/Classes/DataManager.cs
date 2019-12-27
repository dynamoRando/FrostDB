using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FrostCommon;

namespace FrostDB
{
    public class DataManager<TDatabase> where TDatabase : IDatabase
    {
        #region Private Fields
        private List<TDatabase> _databases;
        private string _databaseFolder;
        private string _databaseExtension;
        private IDataFileManager<DataFile> _dataFileManager;
        private IDatabaseFileMapper<TDatabase, DataFile, DataManager<TDatabase>> _databaseFileMapper;
        private IDataManagerEventManager _dataEventManager;
        #endregion

        #region Public Properties
        public List<TDatabase> Databases => _databases;
        #endregion

        #region Events
        #endregion

        #region Protected Methods
        #endregion

        #region Constructors
        public DataManager()
        {
            if (_databaseFileMapper is null)
            {
                _databaseFileMapper = (IDatabaseFileMapper<TDatabase, DataFile, DataManager<TDatabase>>)new DatabaseFileMapper();
            }

            if (_dataFileManager is null)
            {
                _dataFileManager = new DataFileManager();
            }

            _databases = new List<TDatabase>();
            _dataEventManager = new DataManagerEventManager<TDatabase>(this);
            RegisterEvents();
        }
        public DataManager(string databaseFolder,
            string databaseExtension,
            IDatabaseFileMapper<TDatabase, DataFile, DataManager<TDatabase>> mapper) : this()
        {
            _dataFileManager = new DataFileManager();
            _databaseFileMapper = mapper;

            _databaseFolder = databaseFolder;
            _databaseExtension = databaseExtension;

            if (_databaseFileMapper is null)
            {
                _databaseFileMapper = (IDatabaseFileMapper<TDatabase, DataFile, DataManager<TDatabase>>)new DatabaseFileMapper();
            }

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
        public void AddDatabase(TDatabase database)
        {
            if (!HasDatabase(database.Name))
            {
                _databases.Add(database);
                SaveToDisk(database);
            }
        }

        public TDatabase GetDatabase(string databaseName)
        {
            return Databases.Where(d => d.Name == databaseName).FirstOrDefault();
        }

        public TDatabase GetDatabase(Guid? guid)
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
            var db = (TDatabase)ProcessReference.GetDatabase(databaseName);
            _databases.Remove(db);
        }

        public int LoadDatabases(string databaseFolderLocation)
        {
            int count = 0;

            foreach (var file in Directory.GetFiles(databaseFolderLocation))
            {
                var database = GetDatabaseFromDisk(file);
                _databases.Add(database);
                count = Databases.Count;
            }

            return count;
        }

        public void AddToInbox(IMessage message)
        {
            throw new NotImplementedException();
        }

        public void SaveToDisk(TDatabase database)
        {
            var fileName = _databaseFolder + database.Name + _databaseExtension;
            var file = _databaseFileMapper.Map(database);
            _dataFileManager.SaveDataFile(fileName, file);
        }
        #endregion

        #region Private Methods
        private TDatabase GetDatabaseFromDisk(string file)
        {
            var dataFile = _dataFileManager.GetDataFile(file);
            return _databaseFileMapper.Map(dataFile, this);
        }

        private void RegisterEvents()
        {
            _dataEventManager.RegisterEvents();
        }
        #endregion
    }
}
