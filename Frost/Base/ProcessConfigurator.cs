using FrostDB.Enum;
using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace FrostDB.Base
{
    public class ProcessConfigurator : IProcessConfigurator
    {
        #region Private Fields
        private IProcessInfo _info;
        private IConfigurationDefault _default;
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
        }
        #endregion

        #region Public Methods
        public virtual IProcessConfiguration GetConfiguration()
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
        private void SaveConfiguration(IProcessConfiguration configuration)
        {
            ConfigurationManager.SaveConfiguration((Configuration)configuration);
        }
        #endregion
    }
}
