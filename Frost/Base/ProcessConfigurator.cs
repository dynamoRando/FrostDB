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
        private Process _process;
        private IConfigurationDefault _default;
        #endregion

        #region Public Properties
        #endregion

        #region Events
        #endregion

        #region Constructors
        public ProcessConfigurator(IProcessInfo info, Process process)
        {
            _info = info;
            _default = new ConfigurationDefault(_info);
            _process = process;
        }
        #endregion

        #region Public Methods
        public virtual IProcessConfiguration GetConfiguration()
        {
            var config = new Configuration(_process);

            if (!_default.ConfigFileExists())
            {
                config.DatabaseFolder = _default.DatabaseFolder;
                config.FileLocation = _default.ConfigurationFileLocation;
                config.Address = _default.IPAddress;
                config.ServerPort = _default.PortNumber;
            }
            else
            {
                config = GetConcreteConfiguration(_default);
            }

            return config;
        }

        public virtual IProcessConfiguration GetConfiguration(ref Guid? guid, ref string name)
        {
            var config = this.GetConfiguration();
            SetInternalDefaults(ref guid, ref name);

            if (!_default.ConfigFileExists())
            {
                SaveConfiguration(config);
            }

            return config;
        }

        #endregion

        #region Private Methods
        private void SaveConfiguration(IProcessConfiguration configuration)
        {
            ConfigurationManager.SaveConfiguration(configuration);
        }
        private static Configuration GetConcreteConfiguration(IConfigurationDefault def)
        {
            return (Configuration)ConfigurationManager.LoadConfiguration(def.ConfigurationFileLocation);
        }

        private void SetInternalDefaults(ref Guid? guid, ref string name)
        {
            if (!_default.ConfigFileExists())
            {
                if (String.IsNullOrEmpty(name))
                {
                    name = _default.Name;
                }
                if (guid is null)
                {
                    guid = Guid.NewGuid();
                }
            }
            else
            {
                var cfg = GetConcreteConfiguration(_default);
                guid = cfg.Id;
                name = cfg.Name;
            }
        }
        #endregion
    }
}
