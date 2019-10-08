﻿using FrostDB.Enum;
using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace FrostDB.Base
{
    public class Process : IProcess<Database, PartialDatabase>
    {
        #region Private Fields
        private ICommService _commService;
        private IDatabaseManager<Database> _databaseManager;
        private IDatabaseManager<PartialDatabase> _partialDatabaseManager;
        private IProcessInfo _info;
        private IProcessConfigurator<Configuration> _configurator;
        #endregion

        #region Public Properties
        public List<Database> Databases => _databaseManager.Databases;
        public IDatabaseManager<Database> DatabaseManager => _databaseManager;
        public IDatabaseManager<PartialDatabase> PartialDatabaseManager => _partialDatabaseManager;
        public Guid? Id { get => Configuration.Id; }
        public string Name { get => Configuration.Name; }
        public IProcessConfiguration Configuration { get; private set; }
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Process()
        {
            _info = new ProcessInfo(OperatingSystem.GetOSPlatform());
            _configurator = new ProcessConfigurator(_info);
            Configuration = _configurator.GetConfiguration();
            _databaseManager = new DatabaseManager(Configuration.DatabaseFolder, 
                Configuration.DatabaseExtension);
            _partialDatabaseManager = new PartialDatabaseManager(Configuration.DatabaseFolder, 
                Configuration.PartialDatabaseExtension);
        }

        public virtual void AddDatabase(string databaseName)
        {
            _databaseManager.AddDatabase(new Database(databaseName, _databaseManager));
        }

        public virtual void RemoveDatabase(Guid guid)
        {
            _databaseManager.RemoveDatabase(guid);
        }

        public virtual void RemoveDatabase(string databaseName)
        {
            _databaseManager.RemoveDatabase(databaseName);
        }

        public virtual int LoadDatabases()
        {
            return _databaseManager.LoadDatabases(Configuration.DatabaseFolder);
        }

        public virtual IDatabase GetDatabase(Guid id)
        {
            return _databaseManager.GetDatabase(id);
        }
        public virtual Database GetDatabase(string databaseName)
        {
            return _databaseManager.GetDatabase(databaseName);
        }

        public virtual PartialDatabase GetPartialDatabase(string databaseName)
        {
            return _partialDatabaseManager.GetDatabase(databaseName);
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

    }
}
