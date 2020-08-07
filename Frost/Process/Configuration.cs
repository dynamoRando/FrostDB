using FrostCommon;
using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class Configuration : IProcessConfiguration
    {
        #region Private Fields
        private IConfigurationManager<Configuration> _configManager;
        #endregion

        #region Public Properties
        public string FileLocation { get; set; }
        public string DatabaseFolder { get; set; }
        public string DatabaseExtension { get; set; }
        public string Address { get; set; }
        public int DataServerPort { get; set; }
        public int ConsoleServerPort { get; set; }
        public string Name { get; set; }
        public Guid? Id { get; set; }
        public string PartialDatabaseExtension { get; set; }
        public string ContractExtension { get; set ; }
        public string ContractFolder { get ; set; }
        public string DatabaseDirectoryFileName { get; set; }
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Configuration()
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
