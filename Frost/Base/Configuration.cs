using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
{
    public class Configuration : IConfiguration, INetworkConfiguration
    {
        #region Private Fields
        private IProcess _process;
        #endregion

        #region Public Properties
        public string FileLocation { get; set; }
        public string DatabaseFolder { get; set; }
        public string Address { get; set; }
        public int ServerPort { get; set; }
        public string Name { get { return _process.Name; } }
        public Guid Id { get { return _process.Id; } }
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Configuration(IProcess process)
        {
            _process = process;
        }
        #endregion

        #region Public Methods
        public ILocation GetLocation()
        {
            return new Location(_process.Id, Address, ServerPort, _process.Name) ;
        }
        #endregion

        #region Private Methods
        #endregion
    }
}
