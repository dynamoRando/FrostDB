using FrostDB.Enum;
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
        private IConfiguration _configuration;
        private ICommService _commService;
        private Guid? _id;
        private string _name;
        private IDatabaseManager _databaseManager;
        private IProcessInfo _info;
        #endregion

        #region Public Properties
        public List<IDatabase> Databases => _databaseManager.Databases;
        public Guid? Id => _id;
        public string Name => _name;
        public IConfiguration Configuration => _configuration;
        public ProcessType ProcessType => _info.Type;
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Process()
        {
            NewInternalFields();
        }
        public Process(ProcessType type) : base()
        {
            _info = new ProcessInfo(OperatingSystem.GetOSPlatform(), type);
            Configure(_info.Type);
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

        private void Configure(ProcessType type)
        {
            var operatingSystem = OperatingSystem.GetOSPlatform();
            var configurator = new ProcessConfigurator(operatingSystem, type, this);

            _configuration = configurator.GetConfiguration(ref _id, ref _name);

            SaveConfigIfNeeded(type, operatingSystem);
        }

        private void SaveConfigIfNeeded(ProcessType type, OSPlatform operatingSystem)
        {
            var def = new ConfigurationDefault(_info);
            if (!def.ConfigFileExists())
            {
                ConfigurationManager.SaveConfiguration(_configuration);
            }
        }

        #endregion

    }
}
