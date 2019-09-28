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
        private OSPlatform _operatingSystem;
        private ProcessType _processType;
        private Process _process;
        #endregion

        #region Public Properties
        #endregion

        #region Events
        #endregion

        #region Constructors
        public ProcessConfigurator(OSPlatform os, ProcessType type, Process process)
        {
            _operatingSystem = os;
            _processType = type;
            _process = process;
        }
        #endregion

        #region Public Methods
        public virtual IConfiguration GetConfiguration()
        {
            var config = new Configuration(_process);

            var def = new ConfigurationDefault(_operatingSystem, _processType);
            if (!def.ConfigFileExists())
            {
                config.DatabaseFolder = def.DatabaseFolder;
                config.FileLocation = def.ConfigurationFileLocation;
            }
            else
            {
                config = (Configuration)ConfigurationManager.LoadConfiguration(def.ConfigurationFileLocation);
            }

            return config;
        }

        public virtual IConfiguration GetConfiguration(ref Guid? guid, ref string name)
        {
            var config = this.GetConfiguration();
            SetInternalDefaults(ref guid, ref name);

            return config;
        }


        #endregion

        #region Private Methods
        private void SetInternalDefaults(ref Guid? guid, ref string name)
        {
            var def = new ConfigurationDefault(_operatingSystem, _processType);

            if (!def.ConfigFileExists())
            {
                if (String.IsNullOrEmpty(name))
                {
                    name = def.Name;
                }
                if (guid is null)
                {
                    guid = Guid.NewGuid();
                }
            }
            else
            {
                var cfg = (Configuration)ConfigurationManager.LoadConfiguration(def.ConfigurationFileLocation);
                guid = cfg.Id;
                name = cfg.Name;
            }
        }
        #endregion

    }
}
