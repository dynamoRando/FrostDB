using FrostDB.Enum;
using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace FrostDB.Base
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
                config.Get(_default.ConfigurationFileLocation);
            }
            else
            {
                config.DatabaseFolder = _default.DatabaseFolder;
                config.FileLocation = _default.ConfigurationFileLocation;
                config.Address = _default.IPAddress;
                config.ServerPort = _default.PortNumber;
                config.Id = Guid.NewGuid();
                config.Name = _default.Name;

                SaveConfiguration(config);
            }

            return config;
        }

        #endregion

        #region Private Methods
        private void SaveConfiguration(Configuration configuration)
        {
            _configManager.SaveConfiguration(configuration);
        }
        #endregion
    }
}
