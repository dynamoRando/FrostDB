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
        private IProcessConfiguration _configuration;
        private ICommService _commService;
        private IDatabaseManager _databaseManager;
        private IProcessInfo _info;
        #endregion

        #region Public Properties
        public List<IDatabase> Databases => _databaseManager.Databases;
        public Guid? Id { get => _configuration.Id; }
        public string Name { get => _configuration.Name; }
        public IProcessConfiguration Configuration => _configuration;
        public ProcessType ProcessType => _info.Type;

        #endregion

        #region Events
        #endregion

        #region Constructors
        public Process(ProcessType type)
        {
            NewInternalFields();
            _info = new ProcessInfo(OperatingSystem.GetOSPlatform(), type);
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
            return _databaseManager.LoadDatabases(_configuration.DatabaseFolder);
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        private void NewInternalFields()
        {
            _databaseManager = new DatabaseManager();
        }

        private void Configure()
        {
            var configurator = new ProcessConfigurator(_info);
            _configuration = configurator.GetConfiguration();
        }
        #endregion

    }
}
