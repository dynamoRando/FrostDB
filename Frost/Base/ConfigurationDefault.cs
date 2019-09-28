using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using FrostDB.Enum;

namespace FrostDB.Base
{
    public class ConfigurationDefault : IConfigurationDefault
    {
        #region Private Fields
        private IProcessInfo _info;
        private string _configFileLocation;
        private string _dbFolder;
        private string _dbext;
        private string _name;
        private string _ipAddress;
        private int _portNumber;
        #endregion

        #region Public Properties
        public string ConfigurationFileLocation => _configFileLocation;
        public string DatabaseFolder => _dbFolder;
        public string DatabaseExtension => _dbext;
        public string Name => _name;
        public string IPAddress => _ipAddress;
        public int PortNumber => _portNumber;

        #endregion

        #region Events
        #endregion

        #region Constructors
        public ConfigurationDefault(IProcessInfo info)
        {
            _info = info;
            SetDefaults();
        }
        #endregion

        #region Public Methods
        public virtual bool ConfigFileExists()
        {
            return File.Exists(_configFileLocation);
        }
        #endregion

        #region Private Methods
        private void SetDefaults()
        {
            if (_info.OS == OSPlatform.Windows && _info.Type == ProcessType.Host)
            {
                _configFileLocation = @"C:\FrostDB\config\frost.config";
                _dbFolder = @"C:\FrostDB\dbs\";
                _dbext = ".frost";
                _name = "FrostHost";
                _portNumber = 516;
                _ipAddress = "127.0.0.1";
            }
            else if (_info.OS == OSPlatform.Windows && _info.Type == ProcessType.Store)
            {
                _configFileLocation = @"C:\FrostDB\partner\config\frostStore.config";
                _dbFolder = @"C:\FrostDB\partner\dbs\";
                _dbext = ".frostPart";
                _name = "FrostStore";
                _ipAddress = "127.0.0.1";
                _portNumber = 517;
            }
            else
            {
                throw new NotImplementedException("default not set for OS and process type");
            }
        }
        #endregion
    }
}
