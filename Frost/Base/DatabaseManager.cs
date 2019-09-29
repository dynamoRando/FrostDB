using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace FrostDB.Base
{
    public class DatabaseManager : IDatabaseManager<Database>
    {
        #region Private Fields
        private IDataFileManager<DataFile> _dataFileManager;
        private IDatabaseFileMapper<Database, DataFile, DatabaseManager> _databaseFileMapper;
        private string _databaseFolder;
        private string _databaseExtension;
        #endregion

        #region Public Properties
        public List<Database> Databases { get; }
        public IDataInboxManager Inbox { get; }
        #endregion

        #region Events
        #endregion

        #region Constructors
        public DatabaseManager() { }

        public DatabaseManager(string databaseFolder, string databaseExtension)
        {
            Databases = new List<Database>();
            Inbox = new DataInboxManager();
            _dataFileManager = new DataFileManager();
            _databaseFileMapper = new DatabaseFileMapper();

            _databaseFolder = databaseFolder;
            _databaseExtension = databaseExtension;
        }
        #endregion

        #region Public Methods
        public void AddDatabase(Database database)
        {
            if (!HasDatabase(database.Name))
            {
                Databases.Add(database);
                SaveToDisk(database);
            }
        }

        public Database GetDatabase(string databaseName)
        {
            return Databases.Where(d => d.Name == databaseName).First();
        }

        public Database GetDatabase(Guid guid)
        {
            return Databases.Where(d => d.Id == guid).First();
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
            throw new NotImplementedException();
        }

        public int LoadDatabases(string databaseFolderLocation)
        {
            int count = 0;

            foreach (var file in Directory.GetFiles(databaseFolderLocation))
            {
                var database = GetDatabaseFromDisk(file);
                Databases.Add(database);
                count = Databases.Count;
            }

            return count;
        }

        public void AddToInbox(IMessage message)
        {
            Inbox.AddToInbox((DataMessage)message);
        }

        #endregion

        #region Private Methods
        private Database GetDatabaseFromDisk(string file)
        {
            var dataFile = _dataFileManager.GetDataFile(file);
            return _databaseFileMapper.Map(dataFile, this);
        }

        private void SaveToDisk(Database database)
        {
            var fileName = _databaseFolder + database.Name + _databaseExtension;

            if (!File.Exists(fileName))
            {
                var file = _databaseFileMapper.Map(database);
                _dataFileManager.SaveDataFile(fileName, file);
            }
            else
            {
                throw new IOException("database already exists");
            }
        }

        #endregion




    }
}
