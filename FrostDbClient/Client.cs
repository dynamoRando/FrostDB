using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

namespace FrostDbClient
{
    internal class Client
    {
        #region Private Fields
        private static ManualResetEvent connectDone =
        new ManualResetEvent(false);
        private static ManualResetEvent sendDone =
            new ManualResetEvent(false);
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
        public static void Send(Message message)
        {
            Send(message.Destination, message);
        }
        public static void Send(Location location, Message message)
        {
            try
            {
                connectDone.Reset();
                sendDone.Reset();

                // Establish the remote endpoint for the socket.  
                IPAddress ipAddress = IPAddress.Parse(location.IpAddress);
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, location.PortNumber);

                // Create a TCP/IP socket.  
                Socket client = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect to the remote endpoint.  
                client.BeginConnect(remoteEP,
                    new AsyncCallback(ConnectCallback), client);
                connectDone.WaitOne();

                // Send test data to the remote device.  
                if (client.Connected)
                {
                    Send(client, message);
                    sendDone.WaitOne();
                    // Release the socket.  
                    client.Shutdown(SocketShutdown.Both);
                    client.Close();
                    client.Dispose();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        #endregion

        #region Private Methods
        private static void Send(Socket client, Message message)
        {
            var data = Json.SeralizeMessage(message);

            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), client);
        }

        private static void SendCallback(IAsyncResult ar)
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

        private static void ConnectCallback(IAsyncResult ar)
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
