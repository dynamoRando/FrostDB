using FrostCommon;
using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class FrostConfiguration : IProcessConfiguration
    {
        #region Private Fields
        private IConfigurationManager<FrostConfiguration> _configManager;
        #endregion

        #region Public Properties
        /// <summary>
        /// The default location for "frost.config"
        /// </summary>
        public string FileLocation { get; set; }

        /// <summary>
        /// The location of the "dbs" folder (where database files will be stored)
        /// </summary>
        public string DatabaseFolder { get; set; }

        /// <summary>
        /// (old) The file extension for databases
        /// </summary>
        public string DatabaseExtension { get; set; }

        /// <summary>
        /// The IP Address of this Frost process
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// The data port number for this Frost process. This port is used for FrostDb to FrostDb communication.
        /// </summary>
        public int DataServerPort { get; set; }

        /// <summary>
        /// The console port number for this Frost process. This port is used for Client to FrostDb communication.
        /// </summary>
        public int ConsoleServerPort { get; set; }

        /// <summary>
        /// The name of this Process 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The ID of this Process
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// (old) The file extension for partial databases
        /// </summary>
        public string PartialDatabaseExtension { get; set; }

        /// <summary>
        /// The file extension for contract files. These are contracts that a participant has accepted for various 
        /// databases.
        /// </summary>
        public string ContractExtension { get; set; }

        /// <summary>
        /// The contract folder location where contracts will be stored
        /// </summary>
        public string ContractFolder { get; set; }

        /// <summary>
        /// (new) The filename for "frostDbDirectory.frostdir" - the file that holds all the databases this FrostDb
        /// Process is hosting. This file specifies if databases are online or offline.
        /// </summary>
        public string DatabaseDirectoryFileName { get; set; }

        /// <summary>
        /// (new) The file extension for holding database schema information (".frostSchema")
        /// </summary>
        public string SchemaFileExtension { get; set; }

        /// <summary>
        /// (new) The file extension for holding participants of a database (".frostParticpants"). This file
        /// contains their IPAddress and Port Number and if they are pending or accepted of a database's contract.
        /// </summary>
        public string ParticipantFileExtension { get; set; }

        /// <summary>
        /// (new) The file extension for the actual binary data file containing database information
        /// </summary>
        public string FrostBinaryDataExtension { get; set; }

        /// <summary>
        /// (new) The location for the Frost system folder. This folder holds various system databases and information.
        /// </summary>
        public string FrostSystemFolder { get; set; }
        #endregion

        #region Events
        #endregion

        #region Constructors
        public FrostConfiguration()
        {
            _configManager = new ConfigurationManager();
        }
        #endregion

        #region Public Methods
        public Location GetLocation()
        {
            return new Location(Id, Address, DataServerPort, Name);
        }

        #endregion

        #region Private Methods
        #endregion
    }
}
