using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace FrostDB.Base
{
    public class DatabaseManager : IDatabaseManager
    {
        #region Private Fields
        private IDataFileManager<DataFile> _dataFileManager;
        private IDatabaseFileMapper<IDatabase, IDataFile, DatabaseManager> _databaseFileMapper;
        #endregion

        #region Public Properties
        public List<IDatabase> Databases { get; }
        public DataInboxManager Inbox { get; }
        #endregion

        #region Events
        #endregion

        #region Constructors
        public DatabaseManager()
        {
            Databases = new List<IDatabase>();
            Inbox = new DataInboxManager();
            _dataFileManager = new DataFileManager();
            _databaseFileMapper = new DatabaseFileMapper();
        }
        #endregion

        #region Public Methods
        public void AddDatabase(Database database)
        {
            Databases.Add(database);
        }

        public IDatabase GetDatabase(string databaseName)
        {
            return Databases.Where(d => d.Name == databaseName).First();
        }

        public IDatabase GetDatabase(Guid guid)
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
        private IDatabase GetDatabaseFromDisk(string file)
        {
            var dataFile = _dataFileManager.GetDataFile(file);
            return _databaseFileMapper.Map(dataFile, this);
        }

        #endregion




    }
}
