using System;
using System.Net;

namespace FrostDbClient
{
    public class FrostClient
    {
        #region Private Fields
        IPAddress _localIpAddress;
        IPAddress _remoteIpAddress;
        int _remotePortNumber;
        int _localPortNumber;
        #endregion

        #region Public Properties
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public FrostClient(IPAddress remoteIpAddress, IPAddress localIpAddress, int remotePortNumber, int localPortNumber)
        {
            _remoteIpAddress = remoteIpAddress;
            _remotePortNumber = remotePortNumber;
            _localPortNumber = localPortNumber;
            _localIpAddress = localIpAddress;
        }
        #endregion

        #region Public Methods
        public void GetDatabases()
        {
            throw new NotImplementedException();
        }
        public void Connect()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        #endregion


    }
}
