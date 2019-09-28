using FrostDB.Enum;
using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
{
    public class Configuration : IProcessConfiguration
    {
        #region Private Fields
        #endregion

        #region Public Properties
        public string FileLocation { get; set; }
        public string DatabaseFolder { get; set; }
        public string Address { get; set; }
        public int ServerPort { get; set; }
        public string Name { get; set; }
        public Guid? Id { get; set; }
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Configuration()
        {
        }
        #endregion

        #region Public Methods
        public ILocation GetLocation()
        {
            return new Location(Id, Address, ServerPort, Name) ;
        }

        public void Get(string configLocation)
        {
            var loadedConfig = (Configuration)ConfigurationManager.LoadConfiguration(configLocation);
            Map(loadedConfig);
        }
        #endregion

        #region Private Methods
        private void Map(Configuration config)
        {
            this.DatabaseFolder = config.DatabaseFolder;
            this.FileLocation = config.FileLocation;
            this.Address = config.Address;
            this.ServerPort = config.ServerPort;
            this.Id = config.Id;
            this.Name = config.Name;
        }
        #endregion
    }
}
