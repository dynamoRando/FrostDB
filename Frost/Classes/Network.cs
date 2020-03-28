using System;
using System.Collections.Concurrent;
using FrostCommon;
using FrostCommon.Net;
using FrostDB.EventArgs;

namespace FrostDB
{
    public class Network
    {
        #region Private Fields
        private ConcurrentBag<Guid?> _messageIds;
        MessageDataProcessor _messageDataProcessor;
        MessageConsoleProcessor _messageConsoleProcessor;
        Server _dataServer;
        Server _consoleServer;
        Process _process;
        Client _client;
        #endregion

        #region Public Properties
        public const double QUEUE_TIMEOUT = 30.0;
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Network(Process process)
        {
            _messageIds = new ConcurrentBag<Guid?>();
            _client = new Client();
            _process = process;
            _messageConsoleProcessor = new MessageConsoleProcessor(_process);
            _messageDataProcessor = new MessageDataProcessor(_process);
            _dataServer = new Server();
            _dataServer.ServerName = "Data";
            _consoleServer = new Server();
            _consoleServer.ServerName = "Console";
        }
        #endregion

        #region Public Methods
        public void StartDataServer()
        {
            _messageDataProcessor.PortNumber = _process.GetConfiguration().DataServerPort;
            _dataServer.PortNumber = _process.GetConfiguration().DataServerPort;
            _dataServer.Start(_process.GetConfiguration().DataServerPort, _process.GetConfiguration().Address, _messageDataProcessor);
        }

        public void StopDataServer()
        {
            _dataServer.Stop();
        }

        public void StartConsoleServer()
        {
            _messageConsoleProcessor.PortNumber = _process.GetConfiguration().ConsoleServerPort;
            _consoleServer.PortNumber = _process.GetConfiguration().ConsoleServerPort;
            _consoleServer.Start(_process.GetConfiguration().ConsoleServerPort, _process.GetConfiguration().Address, _messageConsoleProcessor);
        }

        public void StopConsoleServer()
        {
            _consoleServer.Stop();
        }
     
        public Guid? SendMessage(Message message)
        {
            Guid? id = message.Id;
            AddToQueue(id);
            _client.Send(message, ClientConstants.TimeOut);
            _process.EventManager.TriggerEvent(EventName.Message.Message_Sent, CreateMessageSentEventArgs(message));
            return id;
        }
        public void AddToQueue(Guid? id)
        {
            _messageIds.Add(id);
        }
        public void RemoveFromQueue(Guid? id)
        {
            _messageIds.TryTake(out id);
        }
        public bool HasMessageId(Guid? id)
        {
            return _messageIds.TryPeek(out id);
        }
        #endregion

        #region Private Methods

        private static MessageSentEventArgs CreateMessageSentEventArgs(Message message)
        {
            string data = Json.SeralizeMessage(message);
            return new MessageSentEventArgs
            {
                Message = message,
                MessageLength = data.Length,
                StringMessage = data
            };
        }
        #endregion

    }
}
