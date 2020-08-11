using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using FrostDB.Enum;

namespace FrostDB
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
        private int _dataPortNumber;
        private int _consolePortNumber;
        private string _appPath;
        private string _contractFolder;
        private string _contractext;
        private string _partialDBextension;
        private string _dbDirectoryFileName;
        private string _schemaFileExtension;
        private string _particpantFileExtension;
        private string _dbAddressBookExtension;
        #endregion

        #region Public Properties
        public string ConfigurationFileLocation => _configFileLocation;
        public string DatabaseFolder => _dbFolder;
        public string DatabaseExtension => _dbext;
        public string Name => _name;
        public string IPAddress => _ipAddress;
        public int DataPortNumber => _dataPortNumber;
        public int ConsolePortNumber => _consolePortNumber;
        public string AppPath => _appPath;
        public string ContractFolder => _contractFolder;
        public string ContractExtension => _contractext;
        public string PartialDatabaseExtension => _partialDBextension;
        public string DatabaseDirectoryFileName => _dbDirectoryFileName;
        public string SchemaFileExtension => _schemaFileExtension;
        public string ParticpantFileExtension => _particpantFileExtension;
        public string DatabaseAddressBookExtension => _dbAddressBookExtension;
        #endregion

        #region Events
        #endregion

        #region Constructors
        public ConfigurationDefault(IProcessInfo info)
        {
            _info = info;
            _appPath = Directory.GetCurrentDirectory();
            SetDefaults();
            CreateDirectories();
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
            if (_info.OS == OSPlatform.Windows)
            {
                //_configFileLocation = _appPath + @"\frost.config";
                _configFileLocation = Path.Combine(_appPath, "frost.config");
                //_dbFolder = _appPath + @"\dbs\";
                _dbFolder = Path.Combine(_appPath, "dbs");
                //_contractFolder = _appPath + @"\contracts\";
                _contractFolder = Path.Combine(_appPath, "contracts");
                _dbext = ".frost";
                _contractext = ".frostContract";
                _name = "FrostHost";
                _dataPortNumber = 516;
                _consolePortNumber = 519;
                _ipAddress = "127.0.0.1";
                _partialDBextension = ".frostPart";
                _dbDirectoryFileName = "frostDbDirectory.frostdir";
                _schemaFileExtension = ".frostSchema";
                _particpantFileExtension = ".frostParticpants";
                _dbAddressBookExtension = ".frostDbAddrDir";
            }
            else if (_info.OS == OSPlatform.Linux)
            {
                //_configFileLocation = _appPath + @"/frost.config";
                _configFileLocation = Path.Combine(_appPath, "frost.config");
                //_dbFolder = _appPath + @"/dbs/";
                _dbFolder = Path.Combine(_appPath, "dbs");
                //_contractFolder = _appPath + @"/contracts/";
                _contractFolder = Path.Combine(_appPath, "contracts");
                _dbext = ".frost";
                _contractext = ".frostContract";
                _name = "FrostHost";
                _dataPortNumber = 516;
                _consolePortNumber = 519;
                _ipAddress = "127.0.0.1";
                _partialDBextension = ".frostPart";
                _dbDirectoryFileName = "frostDbDirectory.frostdir";
                _schemaFileExtension = ".frostSchema";
                _particpantFileExtension = ".frostParticpants";
                _dbAddressBookExtension = ".frostDbAddrDir";
            }
            else
            {
                throw new NotImplementedException("default not set for OS");
            }
        }

        private void CreateDirectories()
        {
            Directory.CreateDirectory(_dbFolder);
            Directory.CreateDirectory(_contractFolder);
        }
        #endregion
    }
}
