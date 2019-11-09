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
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public int PortNumber { get; set; }
        public string Url { get; set; }
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Location(Guid? id, string ipAddress, int portNumber, string name)
        {
            Id = id;
            IpAddress = ipAddress;
            PortNumber = portNumber;
            Name = name;
        }
        #endregion

        #region Public Methods
        public bool IsLocal()
        {
            if (IpAddress.Contains("127.0.0.1") || Url.Contains("localhost") || (IpAddress == Process.GetLocation().IpAddress && PortNumber == Process.GetLocation().PortNumber))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Private Methods
        #endregion
    }
}
