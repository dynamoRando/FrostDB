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
        private OSPlatform _os;
        private string _configFileLocation;
        private string _dbFolder;
        private ProcessType _type;
        private string _dbext;
        private string _name;
        #endregion

        #region Public Properties
        public string ConfigurationFileLocation => _configFileLocation;
        public string DatabaseFolder => _dbFolder;
        public string DatabaseExtension => _dbext;
        public string Name => _name;
        #endregion

        #region Events
        #endregion

        #region Constructors
        public ConfigurationDefault(OSPlatform os, ProcessType type)
        {
            _os = os;
            _type = type;
            SetDefault();
        }
        #endregion

        #region Public Methods
        public virtual bool ConfigFileExists()
        {
            return File.Exists(_configFileLocation);
        }
        #endregion

        #region Private Methods
        private void SetDefault()
        {
            if (_os == OSPlatform.Windows && _type == ProcessType.Host)
            {
                _configFileLocation = @"C:\FrostDB\config\frost.config";
                _dbFolder = @"C:\FrostDB\dbs\";
                _dbext = ".frost";
                _name = "FrostHost";
            }
            if (_os == OSPlatform.Windows && _type == ProcessType.Store)
            {
                _configFileLocation = @"C:\FrostDB\partner\config\frostStore.config";
                _dbFolder = @"C:\FrostDB\partner\dbs\";
                _dbext = ".frostPart";
                _name = "FrostStore";
            }
            else
            {
                throw new NotImplementedException("default not set for OS and process type");
            }
        }
        #endregion
    }
}
