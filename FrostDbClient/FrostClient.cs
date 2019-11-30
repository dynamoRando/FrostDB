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
        MessageClientConsoleProcessor _processor;
        Location _local;
        Location _remote;
        FrostClientInfo _info;
        EventManager _eventManager;
        #endregion

        #region Public Properties
        public FrostClientInfo Info => _info;
        public EventManager EventManager => _eventManager;
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

            _eventManager = new EventManager();

            _local = new Location(null, _localIpAddress, _localPortNumber, "FrostDbClient");
            _remote = new Location(null, _remoteIpAddress, _remotePortNumber, string.Empty);
            _info = new FrostClientInfo();
            _processor = new MessageClientConsoleProcessor(ref _info, ref _eventManager);
            
            SetupServer();
        }
        #endregion

        #region Public Methods
        public void GetProcessId()
        {
            string messageContent = string.Empty;

            Message response = new Message(
                destination: _remote,
                origin: _local,
                messageContent: messageContent,
                messageAction: MessageConsoleAction.Process.Get_Id,
                messageType: MessageType.Console);

            Client.Send(response);
        }
        public void GetDatabases()
        {
            string messageContent = string.Empty;
            
            Message response = new Message(
                destination: _remote,
                origin: _local,
                messageContent: messageContent,
                messageAction: MessageConsoleAction.Process.Get_Databases,
                messageType: MessageType.Console);

            Client.Send(response);

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
            _localServer.Start(_localPortNumber, _localIpAddress, _processor);
        }
        #endregion


    }
}
