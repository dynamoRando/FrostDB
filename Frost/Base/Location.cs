using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
{
    public class Location : ILocation, IFrostObject
    {
        #region Private Fields
        #endregion

        #region Public Properties
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public int PortNumber { get; set; }
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Location(Guid id, string ipAddress, int portNumber, string name)
        {
            Id = id;
            IpAddress = ipAddress;
            PortNumber = portNumber;
            Name = name;
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion
    }
}
