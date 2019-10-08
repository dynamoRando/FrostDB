using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
{
    public class PartialDatabaseManager : IDatabaseManager<PartialDatabase>
    {
        #region Private Fields
        private string _databaseFolder;
        private string _databaseExtension;
        #endregion

        #region Public Properties
        public List<PartialDatabase> Databases => throw new NotImplementedException();
        public IDataInboxManager Inbox => throw new NotImplementedException();
        #endregion

        #region Events
        #endregion

        #region Constructors
        public PartialDatabaseManager(string databaseFolder, string databaseExtension)
        {
            _databaseExtension = databaseExtension;
            _databaseFolder = databaseFolder;
        }
        #endregion

        #region Public Methods
        public void AddDatabase(PartialDatabase database)
        {
            throw new NotImplementedException();
        }

        public void AddToInbox(IMessage message)
        {
            throw new NotImplementedException();
        }

        public PartialDatabase GetDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }

        public PartialDatabase GetDatabase(Guid guid)
        {
            throw new NotImplementedException();
        }

        public bool HasDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }

        public bool HasDatabase(Guid guid)
        {
            throw new NotImplementedException();
        }

        public int LoadDatabases(string databaseFolderLocation)
        {
            throw new NotImplementedException();
        }

        public void RemoveDatabase(Guid guid)
        {
            throw new NotImplementedException();
        }

        public void RemoveDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        #endregion





    }
}
