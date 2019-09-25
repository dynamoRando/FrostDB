using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
{
    public class DatabaseManager : IDatabaseManager
    {
        #region Private Fields
        private List<IDatabase> _databases;
        #endregion

        #region Public Properties
        public List<IDatabase> Databases => _databases;
        #endregion

        #region Events
        #endregion

        #region Constructors
        public DatabaseManager()
        {
            _databases = new List<IDatabase>();
        }
        #endregion

        #region Public Methods
        public void AddDatabase(IDatabase database)
        {
            throw new NotImplementedException();
        }

        public IDatabase GetDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }

        public IDatabase GetDatabase(Guid guid)
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
