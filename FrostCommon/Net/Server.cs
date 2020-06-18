using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using FrostCommon;
using System.Diagnostics;

namespace FrostCommon.Net
{
    public class Server
    {
        #region Private Fields
        private static ManualResetEvent _allDone = new ManualResetEvent(false);
        private IMessageProcessor _messageProcessor;
        private Socket _activeSocket;
        private bool _autoDisconnect = true;
        #endregion

        #region Public Fields
        public bool IsRunning = true;
        #endregion

        #region Public Properties
        public string ServerName { get; set; }
        public int PortNumber { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Server() { }
        #endregion

        #region Public Methods

        public void Start(int portNumber, string ipAddress, IMessageProcessor messageProcessor)
        {
            _messageProcessor = messageProcessor;
            Task.Run(() => StartListening(portNumber, ipAddress));
        }

        public void Stop()
        {
            IsRunning = false;
        }

        public void DisconnectSocket()
        {
            DisconnectSocket(_activeSocket);
        }

        public void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.  
            _allDone.Set();

            // Get the socket that handles the client request.  
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            var state = new StateObject();
            state.workSocket = handler;

            //Task.Run(() => GetDataFromSocket(state));
            GetDataFromSocket(state);

            // Create the state object.  
            //StateObject state = new StateObject();
            //state.workSocket = handler;
            //handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
            //    new AsyncCallback(ReadCallback), state);
        }

        public void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.  
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;
            _activeSocket = handler;

            // Read data from the client socket. 
            int bytesRead = 0;

            try
            {
                bytesRead = handler.EndReceive(ar);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Debug.WriteLine(e.ToString());
            }

            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.  
                state.sb.Append(Encoding.ASCII.GetString(
                    state.buffer, 0, bytesRead));

                // Check for end-of-file tag. If it is not there, read   
                // more data.  
                content = state.sb.ToString();

                Debug.WriteLine($"Server - Content: {content}");

                Message message;

                if (Json.TryParse(content, out message))
                {
                    if (content.Length > 5)
                    {
                        message.JsonData = content;
                        _messageProcessor.Process(message);
                    }
                }
                else
                {
                    // Not all data received. Get more.  
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
                }
            }

            if (_autoDisconnect)
            {
                DisconnectSocket(handler);
            }
        }
        #endregion

        #region Private Methods
        private void DisconnectSocket(Socket socket)
        {
            try
            {
                if (socket.IsConnected())
                {
                    Debug.WriteLine($"Server: Disconnecting Socket {socket.LocalEndPoint.ToString()} : {socket.RemoteEndPoint.ToString()}");
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Disconnect(true);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Debug.WriteLine(e.ToString());
            }

            //socket.Close();
        }

        private void GetDataFromSocket(StateObject state)
        {
            Debug.WriteLine("--- Server - GetDataFromSocket ---");

            var socket = state.workSocket;
            //SetKeepAliveOnSocket(socket);

            // while (socket.Connected)
            //{
            try
            {
                socket.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
            }
            catch (SocketException se)
            {
                Debug.WriteLine(se.ToString());
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
            //}
        }

        private void SetKeepAliveOnSocket(Socket socket)
        {
            socket.SetKeepAlive(1000, 2);
        }

        private void StartListening(int portNumber, string ipAddress)
        {
            if (IsRunning == false)
            {
                IsRunning = true;
            }

            IPAddress ip = IPAddress.Parse(ipAddress);
            IPEndPoint localEndPoint = new IPEndPoint(ip, portNumber);
            Socket listener = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listener.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (IsRunning)
                {
                    // Set the event to nonsignaled state.  
                    _allDone.Reset();

                    // Start an asynchronous socket to listen for connections.  
                    //Console.WriteLine("Waiting for a connection...");
                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        listener);

                    // Wait until a connection is made before continuing.  
                    _allDone.WaitOne();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        #endregion
    }
}
