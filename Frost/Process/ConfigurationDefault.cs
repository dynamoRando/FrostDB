using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using FrostDB.Enum;

namespace FrostDB
{
    /// <summary>
    /// Specifies the default values for FrostDB
    /// </summary>
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
        private string _dbFrostBinaryExtension;
        private string _dbFrostBinaryDataDirectoryExtension;
        private string _frostSystemFolder;
        private string _frostSecurityFileExtension;
        #endregion

        #region Public Properties

        /// <summary>
        /// The default location for "frost.config"
        /// </summary>
        public string ConfigurationFileLocation => _configFileLocation;

        /// <summary>
        /// The location of the "dbs" folder (where database files will be stored)
        /// </summary>
        public string DatabaseFolder => _dbFolder;

        /// <summary>
        /// (old) The file extension for databases
        /// </summary>
        public string DatabaseExtension => _dbext;

        /// <summary>
        /// The name of this Process 
        /// </summary>
        public string Name => _name;

        /// <summary>
        /// The IP Address of this Frost process
        /// </summary>
        public string IPAddress => _ipAddress;

        /// <summary>
        /// The data port number for this Frost process. This port is used for FrostDb to FrostDb communication.
        /// </summary>
        public int DataPortNumber => _dataPortNumber;

        /// <summary>
        /// The console port number for this Frost process. This port is used for Client to FrostDb communication.
        /// </summary>
        public int ConsolePortNumber => _consolePortNumber;

        /// <summary>
        /// The root path for where this Frost process is located
        /// </summary>
        public string AppPath => _appPath;

        /// <summary>
        /// The contract folder location where contracts will be stored
        /// </summary>
        public string ContractFolder => _contractFolder;

        /// <summary>
        /// The file extension for contract files. These are contracts that a participant has accepted for various 
        /// databases.
        /// </summary>
        public string ContractExtension => _contractext;

        /// <summary>
        /// (old) The file extension for partial databases
        /// </summary>
        public string PartialDatabaseExtension => _partialDBextension;

        /// <summary>
        /// (new) The filename for "frostDbDirectory.frostdir" - the file that holds all the databases this FrostDb
        /// Process is hosting. This file specifies if databases are online or offline.
        /// </summary>
        public string DatabaseDirectoryFileName => _dbDirectoryFileName;

        /// <summary>
        /// (new) The file extension for holding database schema information (".frostSchema")
        /// </summary>
        public string SchemaFileExtension => _schemaFileExtension;

        /// <summary>
        /// (new) The file extension for holding participants of a database (".frostParticpants"). This file
        /// contains their IPAddress and Port Number and if they are pending or accepted of a database's contract.
        /// </summary>
        public string ParticpantFileExtension => _particpantFileExtension;

        /// <summary>
        /// (new) The file extension for the actual binary data file containing database information
        /// </summary>
        public string FrostBinaryDataExtension => _dbFrostBinaryExtension;

        /// <summary>
        /// (new) The file extension for the page/line number directory for the binary data file
        /// </summary>
        public string FrostBinaryDataDirectoryExtension => _dbFrostBinaryDataDirectoryExtension;

        /// <summary>
        /// (new) The location for the Frost system folder. This folder holds various system databases and information.
        /// </summary>
        public string FrostSystemFolder => _frostSystemFolder;

        /// <summary>
        /// (new) The security file extension for a db. Contains users and permissions.
        /// </summary>
        public string FrostSecurityFileExtension => _frostSecurityFileExtension;
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
                _configFileLocation = Path.Combine(_appPath, "frost.config");
                _dbFolder = Path.Combine(_appPath, "dbs");
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
                _dbFrostBinaryExtension = ".frostDbData";
                _dbFrostBinaryDataDirectoryExtension = ".frostDbDirData";
                _frostSystemFolder = Path.Combine(_appPath, "sys");
                _frostSecurityFileExtension = ".frostDbSecurity";
            }
            else if (_info.OS == OSPlatform.Linux)
            {
                _configFileLocation = Path.Combine(_appPath, "frost.config");
                _dbFolder = Path.Combine(_appPath, "dbs");
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
                _dbFrostBinaryExtension = ".frostDbData";
                _dbFrostBinaryDataDirectoryExtension = ".frostDbDirData";
                _frostSystemFolder = Path.Combine(_appPath, "sys");
                _frostSecurityFileExtension = ".frostDbSecurity";
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
