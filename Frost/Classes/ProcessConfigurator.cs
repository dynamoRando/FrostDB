using FrostDB.Enum;
using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace FrostDB
{
    public class ProcessConfigurator : IProcessConfigurator<Configuration>
    {
        #region Private Fields
        private IProcessInfo _info;
        private IConfigurationDefault _default;
        private IConfigurationManager<Configuration> _configManager;
        #endregion

        #region Public Properties
        #endregion

        #region Events
        #endregion

        #region Constructors
        public ProcessConfigurator(IProcessInfo info)
        {
            _info = info;
            _default = new ConfigurationDefault(_info);
            _configManager = new ConfigurationManager();
        }
        #endregion

        #region Public Methods
        public virtual Configuration GetConfiguration()
        {
            var config = new Configuration();

            if (_default.ConfigFileExists())
            {
                config = _configManager.LoadConfiguration(_default.ConfigurationFileLocation);
            }
            else
            {
                SetDefaultValues(config);
                SaveConfiguration(config);
            }

            return config;
        }

        public virtual Configuration GetConfiguration(string rootDirectory)
        {
            var config = new Configuration();
            var filePath = rootDirectory + @"\" + @"frost.config";

            if (File.Exists(filePath))
            {
                config = _configManager.LoadConfiguration(filePath);
            }
            else
            {
                SetDefaultValues(config, rootDirectory);
                SaveConfiguration(config);
            }

            return config;
        }

        public void SaveConfiguration(Configuration configuration)
        {
            _configManager.SaveConfiguration(configuration);
        }

        public void SetDefaultValues(Configuration config)
        {
            config.DatabaseFolder = _default.DatabaseFolder;
            config.FileLocation = _default.ConfigurationFileLocation;
            config.Address = _default.IPAddress;
            config.DataServerPort = _default.DataPortNumber;
            config.DatabaseExtension = _default.DatabaseExtension;
            config.Id = Guid.NewGuid();
            config.Name = _default.Name;
            config.PartialDatabaseExtension = _default.PartialDatabaseExtension;
            config.ContractExtension = _default.ContractExtension;
            config.ContractFolder = _default.ContractFolder;
            config.ConsoleServerPort = _default.ConsolePortNumber;
        }

        public void SetDefaultValues(Configuration config, string rootDirectory)
        {
            config.DatabaseFolder = rootDirectory + @"\" + @"\dbs\";
            config.FileLocation = rootDirectory + @"\" + @"\frost.config";
            config.Address = _default.IPAddress;
            config.DataServerPort = _default.DataPortNumber;
            config.DatabaseExtension = _default.DatabaseExtension;
            config.Id = Guid.NewGuid();
            config.Name = _default.Name;
            config.PartialDatabaseExtension = _default.PartialDatabaseExtension;
            config.ContractExtension = _default.ContractExtension;
            config.ContractFolder = rootDirectory + @"\" + @"\contracts\";
            config.ConsoleServerPort = _default.ConsolePortNumber;
        }

        #endregion

        #region Private Methods

        #endregion
    }
}
