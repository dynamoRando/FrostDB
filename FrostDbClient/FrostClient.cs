using System;
using System.Diagnostics;
using System.Net;
using FrostCommon;
using FrostCommon.Net;
using System.Threading.Tasks;
using System.Collections.Generic;

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

            _local = new Location(Guid.NewGuid(), _localIpAddress, _localPortNumber, "FrostDbClient");
            _remote = new Location(Guid.NewGuid(), _remoteIpAddress, _remotePortNumber, string.Empty);
            _info = new FrostClientInfo();
            _processor = new MessageClientConsoleProcessor(ref _info, ref _eventManager);
            
            SetupServer();
        }
        #endregion

        #region Public Methods
        public void GetProcessId()
        {
            SendMessage(BuildMessage(string.Empty, MessageConsoleAction.Process.Get_Id));
        }
        
        // i can either call a method and then try and wait for an event to be recieved
        public void GetDatabases()
        {
            SendMessage(BuildMessage(string.Empty, MessageConsoleAction.Process.Get_Databases));
        }

        // or, i can send a message and then check for when the data has come back and return to the caller
        public async Task<List<string>> GetDatabasesAsync()
        {
            var id = SendMessage(BuildMessage(string.Empty, MessageConsoleAction.Process.Get_Databases));
            // really should take the result here and if it's false, then just return an empty list
            await WaitForMessageAsync(id);
            return _info.DatabaseNames;
        }

        public void GetDatabaseInfo(string databaseName)
        {
            SendMessage(BuildMessage(databaseName, MessageConsoleAction.Database.Get_Database_Info));
        }

        public void Connect()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        private async Task<bool> WaitForMessageAsync(Guid? id)
        {
            return await Task.Run(() => WaitForMessage(id));
        }

        private bool WaitForMessage(Guid? id)
        {
            Stopwatch watch = new Stopwatch();
            bool responseRecieved = false;
            double timeOut = 3.0;

            watch.Start();

            while (watch.Elapsed.TotalSeconds < timeOut)
            {
                if (!_info.HasMessageId(id))
                {
                    responseRecieved = true;

                    Debug.WriteLine(watch.Elapsed.TotalSeconds.ToString());
                    Console.WriteLine(watch.Elapsed.TotalSeconds.ToString());

                    break;

                }
                else
                {
                    continue;
                }
            }

            watch.Stop();

            return responseRecieved;
        }

        private Guid? SendMessage(Message message)
        {
            Guid? id = message.Id;
            Client.Send(message);
            _info.AddToQueue(id);

            return id;
        }
        private Message BuildMessage(string content, string action)
        {
            Message message = new Message(
               destination: _remote,
               origin: _local,
               messageContent: content,
               messageAction: action,
               messageType: MessageType.Console);

            return message;
        }
        private void SetupServer()
        {
            _localServer = new Server();
            _localServer.Start(_localPortNumber, _localIpAddress, _processor);
        }
        #endregion


    }
}
