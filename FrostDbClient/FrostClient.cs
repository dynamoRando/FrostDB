using System;
using System.Net;
using FrostCommon;
using FrostCommon.Net;

namespace FrostDbClient
{
    public class FrostClient
    {
        #region Private Fields
        string _localIpAddress;
        string _remoteIpAddress;
        int _remotePortNumber;
        int _localPortNumber;
        Server _localServer;
        #endregion

        #region Public Properties
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public FrostClient(string remoteIpAddress, string localIpAddress, int remotePortNumber, int localPortNumber)
        {
            _remoteIpAddress = remoteIpAddress;
            _remotePortNumber = remotePortNumber;
            _localPortNumber = localPortNumber;
            _localIpAddress = localIpAddress;

            SetupServer();
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
        private void SetupServer()
        {
            _localServer = new Server();
            _localServer.Start(_localPortNumber, _localIpAddress, null);
        }
        #endregion


    }
}
