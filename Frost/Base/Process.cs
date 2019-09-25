using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
{
    public class Process : IProcess
    {
        #region Private Fields
        private IConfiguration _configuration;
        private ICommService _commService;
        private Guid _id;
        private string _name;
        private IDatabaseManager _databaseManager;
        #endregion

        #region Public Properties
        public List<IDatabase> Databases { get { return _databaseManager.Databases; } }
        public Guid Id { get { return _id; } }
        public string Name { get { return _name; } }
        public IConfiguration Configuration { get { return _configuration; } }
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Process()
        {
            _databaseManager = new DatabaseManager();
        }

        public void AddDatabase(IDatabase database)
        {
            _databaseManager.AddDatabase(database);
        }

        public void RemoveDatabase(Guid guid)
        {
            _databaseManager.RemoveDatabase(guid);
        }

        public void RemoveDatabase(string databaseName)
        {
            _databaseManager.RemoveDatabase(databaseName);
        }

        public int LoadDatabases()
        {
            return _databaseManager.LoadDatabases(_configuration.DatabaseFolder);
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

    }
}
