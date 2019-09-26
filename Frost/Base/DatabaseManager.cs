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
        private List<IDatabase> _databases;
        private DataInboxManager _inboxManager;
        #endregion

        #region Public Properties
        public List<IDatabase> Databases => _databases;
        public DataInboxManager Inbox => _inboxManager;
        #endregion

        #region Events
        #endregion

        #region Constructors
        public DatabaseManager()
        {
            _databases = new List<IDatabase>();
            _inboxManager = new DataInboxManager();
        }
        #endregion

        #region Public Methods
        public void AddDatabase(Database database)
        {
            _databases.Add(database);
        }

        public IDatabase GetDatabase(string databaseName)
        {
            return _databases.Where(d => d.Name == databaseName).First();
        }

        public IDatabase GetDatabase(Guid guid)
        {
            return _databases.Where(d => d.Id == guid).First();
        }

        public bool HasDatabase(string databaseName)
        {
            return _databases.Any(d => d.Name == databaseName);
        }

        public bool HasDatabase(Guid guid)
        {
            return _databases.Any(d => d.Id == guid);
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
                _databases.Add(database);
                count = _databases.Count;
            }

            return count;
        }

        public void AddToInbox(IMessage message)
        {
            _inboxManager.AddToInbox((DataMessage)message);
        }

        #endregion

        #region Private Methods
        private IDatabase GetDatabaseFromDisk(string file)
        {
            throw new NotImplementedException();
        }

        #endregion




    }
}
