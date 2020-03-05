using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using FrostCommon;
using System.Diagnostics;

namespace FrostCommon.Net
{
    public class Client
    {
        #region Private Fields
        private ManualResetEvent connectDone =
        new ManualResetEvent(false);
        private ManualResetEvent sendDone =
            new ManualResetEvent(false);
        private int _timeout;
        private ManualResetEvent receiveDone = new ManualResetEvent(false);
        private String response = String.Empty;
        private Socket _client;
        #endregion

        #region Public Properties
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        public void Send(Message message, int timeout)
        {
            _timeout = timeout;
            Send(message.Destination, message);
        }
        public void Send(Location location, Message message)
        {
            try
            {
                connectDone.Reset();
                sendDone.Reset();

                // Establish the remote endpoint for the socket.  
                IPAddress ipAddress = IPAddress.Parse(location.IpAddress);
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, location.PortNumber);

                using (var _client = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
                {
                    // Connect to the remote endpoint.  
                    _client.BeginConnect(remoteEP,
                        new AsyncCallback(ConnectCallback), _client);
                    connectDone.WaitOne(_timeout);

                    // Send test data to the remote device.  
                    if (_client.Connected)
                    {
                        Send(_client, message);
                        sendDone.WaitOne(_timeout);

                        // Receive the response from the remote device.  
                        //Receive(client);
                        //receiveDone.WaitOne();

                        // Release the socket.  
                        _client.Shutdown(SocketShutdown.Both);
                        _client.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Debug.Write(e.ToString());
            }
        }
        //public  void Send(Participant participant, Message message)
        //{
        //    Send(participant.Location, message);
        //}
        #endregion


        #region Private Methods
        private void Receive(Socket client)
        {
            try
            {
                // Create the state object.  
                StateObject state = new StateObject();
                state.workSocket = client;

                // Begin receiving the data from the remote device.  
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket   
                // from the asynchronous state object.  
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;

                // Read data from the remote device.  
                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // There might be more data, so store the data received so far.  
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                    // Get the rest of the data.  
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReceiveCallback), state);
                }
                else
                {
                    // All the data has arrived; put it in response.  
                    if (state.sb.Length > 1)
                    {
                        response = state.sb.ToString();
                    }
                    // Signal that all bytes have been received.  
                    receiveDone.Set();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void Send(Socket client, Message message)
        {
            var data = Json.SeralizeMessage(message);

            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), client);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = client.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);

                // Signal that all bytes have been sent.  
                sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.  
                client.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}",
                    client.RemoteEndPoint.ToString());

                // Signal that the connection has been made.  
                connectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        #endregion

    }
}
