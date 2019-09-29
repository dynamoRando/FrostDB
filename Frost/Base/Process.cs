﻿using FrostDB.Enum;
using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace FrostDB.Base
{
    public class Process : IProcess
    {
        #region Private Fields
        private ICommService _commService;
        private IDatabaseManager _databaseManager;
        private IProcessInfo _info;
        private IProcessConfigurator _configurator;
        #endregion

        #region Public Properties
        public List<IDatabase> Databases => _databaseManager.Databases;
        public Guid? Id { get => Configuration.Id; }
        public string Name { get => Configuration.Name; }
        public IProcessConfiguration Configuration { get; private set; }
        public ProcessType ProcessType => _info.Type;

        #endregion

        #region Events
        #endregion

        #region Constructors
        public Process(ProcessType type)
        {
            NewInternalFields(type);
            Configure();
        }

        public virtual void AddDatabase(Database database)
        {
            _databaseManager.AddDatabase(database);
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
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        private void NewInternalFields(ProcessType type)
        {
            _databaseManager = new DatabaseManager();
            _info = new ProcessInfo(OperatingSystem.GetOSPlatform(), type);
            _configurator = new ProcessConfigurator(_info);
        }

        private void Configure()
        {   
            Configuration = _configurator.GetConfiguration();
        }
        #endregion

    }
}
