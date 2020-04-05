using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading.Tasks;
using FrostCommon;
using FrostCommon.Net;
using FrostDB.EventArgs;

namespace FrostDB
{
    public class Network
    {
        #region Private Fields
        private ConcurrentBag<Guid?> _messageIds;
        private ConcurrentBag<Guid?> _requestMessageIds;
        MessageDataProcessor _messageDataProcessor;
        MessageConsoleProcessor _messageConsoleProcessor;
        Server _dataServer;
        Server _consoleServer;
        Process _process;
        Client _client;
        #endregion

        #region Public Properties
        public const double QUEUE_TIMEOUT = 30.0;
        public MessageDataProcessor DataProcessor => _messageDataProcessor;
        public MessageConsoleProcessor ConsoleProcessor => _messageConsoleProcessor;
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Network(Process process)
        {
            _messageIds = new ConcurrentBag<Guid?>();
            _requestMessageIds = new ConcurrentBag<Guid?>();
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
        public void SendMessageRequestId(Message message, Guid? requestId)
        {
            _requestMessageIds.Add(requestId);
            _client.Send(message, ClientConstants.TimeOut);
            _process.EventManager.TriggerEvent(EventName.Message.Message_Sent, CreateMessageSentEventArgs(message));
        }
        public void AddToQueue(Guid? id)
        {
            _messageIds.Add(id);
        }
        public void RemoveFromQueue(Guid? id)
        {
            _messageIds.TryTake(out id);
        }

        public void RemoveFromQueueToken(Guid? id)
        {
            _requestMessageIds.TryTake(out id);
        }
        public bool HasMessageId(Guid? id)
        {
            return _messageIds.TryPeek(out id);
        }

        public bool HasMessageRequest(Guid? id)
        {
            return _requestMessageIds.TryPeek(out id);
        }

        public async Task<bool> WaitForMessageTokenAsync(Guid? token)
        {
            return await Task.Run(() => WaitForMessageToken(token));
        }

        public async Task<bool> WaitForMessageAsync(Guid? id)
        {
            return await Task.Run(() => WaitForMessage(id));
        }

        #endregion

        #region Private Methods
        private bool WaitForMessageToken(Guid? id)
        {
            Stopwatch watch = new Stopwatch();
            bool responseRecieved = false;

            watch.Start();

            while (watch.Elapsed.TotalSeconds < Network.QUEUE_TIMEOUT)
            {
                if (!_requestMessageIds.TryPeek(out id))
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
        private bool WaitForMessage(Guid? id)
        {
            Stopwatch watch = new Stopwatch();
            bool responseRecieved = false;

            watch.Start();

            while (watch.Elapsed.TotalSeconds < Network.QUEUE_TIMEOUT)
            {
                if (!HasMessageId(id))
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
