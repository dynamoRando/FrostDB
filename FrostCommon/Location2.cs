using System;
using System.Collections.Generic;
using System.Text;

namespace FrostCommon
{
    /// <summary>
    /// Represents a location on the network
    /// </summary>
    public class Location2
    {
        #region Private Fields
        #endregion

        #region Public Properties
        public string IpAddress { get; set; }
        public int PortNumber { get; set; }
        #endregion

        #region Constructors
        public Location2() { }
        public Location2(string ipAddress, int portNumber)
        {
            IpAddress = ipAddress;
            PortNumber = portNumber;
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

    }
}
