using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using FrostCommon;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Collections.Concurrent;
using FrostCommon.ConsoleMessages;

namespace FrostCommon.Net
{
    public class Client
    {
        #region Private Fields
        private String response = String.Empty;
        private ConcurrentDictionary<LocationInfo, SocketHelper> connections;
        private bool _autoDisconnect = false;
        private SocketHelper _currentSocket;
        #endregion

        #region Public Properties
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Client()
        {
            connections = new ConcurrentDictionary<LocationInfo, SocketHelper>();
        }
        #endregion

        #region Public Methods
        public void DisconnectSocket()
        {
                DisconnectSocket(_currentSocket);
        }
        public void Send(Message message, int timeout)
        {
            Send(message.Destination, message, timeout);
        }

        public void Send(Location location, Message message, int timeOut)
        {
            try
            {

                DebugSend(location, message);

                SocketHelper connection;

                if (HasConnection(location))
                {
                    connection = GetConnection(location);
                }
                else
                {
                    connection = GetNewSocketHelper(location, GetNewSocket(location), timeOut);
                    connections.TryAdd(location.Convert(), connection);
                }

                if (!connection.Socket.IsConnected())
                {
                    ConnectSocket(connection);
                    //connection.ConnectDone.WaitOne(connection.TimeOut);
                }

                SendData(connection, message);
                //connection.SendDone.WaitOne(connection.TimeOut);

                if (_autoDisconnect)
                {
                    DisconnectSocket(connection);
                    //connection.DisconnectDone.WaitOne(connection.TimeOut);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Debug.Write(e.ToString());
            }
        }
        #endregion


        #region Private Methods
        private void DebugSend(Location location, Message message)
        {
            Debug.WriteLine($"Client: Destination: {location.IpAddress}:{location.PortNumber.ToString()}");
            Debug.WriteLine($"Client: Message Action {message.Action}");
        }

        private void ConnectSocket(SocketHelper item)
        {
            Debug.WriteLine($"Client: ConnectSocket: {item.Location.IpAddress}:{item.Location.PortNumber.ToString()}");
            try
            {
                item.Socket.BeginConnect(GetEndPoint(item.Location),
                               new AsyncCallback(ConnectCallback), item);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Client: ConnectSocket Error: {e.ToString()}");
            }

            item.ConnectDone.WaitOne(item.TimeOut);
        }

        private void SendData(SocketHelper item, Message message)
        {
            Debug.WriteLine("Client: -- Send Data To: --");
            DebugSend(item.Location, message);
            Send(item, message);
            item.SendDone.WaitOne(item.TimeOut);
        }

        private void Send(SocketHelper item, Message message)
        {

            Debug.WriteLine($"Client: Sending bytes to {item.Location.IpAddress}:{item.Location.PortNumber.ToString()}");

            var data = Json.SeralizeMessage(message);

            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            if (item.Socket.IsConnected())
            {
                item.Socket.BeginSend(byteData, 0, byteData.Length, 0,
                          new AsyncCallback(SendCallback), item);
            }
            else
            {
                Debug.WriteLine($"Client: Send: Socket Disconnected! {item.Location.IpAddress}:{item.Location.PortNumber.ToString()}");
            }
        }

        private void DisconnectSocket(SocketHelper item)
        {
            var client = item.Socket;

            if (client.IsConnected())
            {
                client.Shutdown(SocketShutdown.Both);
                client.BeginDisconnect(true, new AsyncCallback(DisconnectCallback), item);
                item.DisconnectDone.WaitOne(item.TimeOut);
            }
            else
            {
                Debug.WriteLine($"Client: DisconnectSocket: Socket Already Disconnected! {item.Location.IpAddress}:{item.Location.PortNumber.ToString()}");
            }
        }

        private static void DisconnectCallback(IAsyncResult ar)
        {
            SocketHelper item = (SocketHelper)ar.AsyncState;
            Socket client = item.Socket;

            client.EndDisconnect(ar);

            // Signal that the disconnect is complete.
            item.DisconnectDone.Set();
            Debug.WriteLine($"Client: DisconnectCallback done.");
        }

        private IPEndPoint GetEndPoint(Location location)
        {
            IPAddress ipAddress = IPAddress.Parse(location.IpAddress);
            return new IPEndPoint(ipAddress, location.PortNumber);
        }

        private Socket GetNewSocket(Location location)
        {
            IPAddress ipAddress = IPAddress.Parse(location.IpAddress);
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, location.PortNumber);
            var socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            Debug.WriteLine($"Client: --> Making Socket Address: {location.IpAddress} : {location.PortNumber.ToString()}");

            return socket;
        }

        private SocketHelper GetNewSocketHelper(Location location, Socket socket, int timeout)
        {
            var item = new SocketHelper(socket, location, timeout);
            item.ConnectDone.Reset();
            item.SendDone.Reset();
            item.DisconnectDone.Reset();
            return item;
        }

        private bool HasConnection(Location location)
        {
            return connections.ContainsKey(location.Convert());
        }

        private SocketHelper GetConnection(Location location)
        {
            SocketHelper item;
            connections.TryRemove(location.Convert(), out item);
            return item;
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                SocketHelper item = (SocketHelper)ar.AsyncState;
                Socket client = item.Socket;

                // Complete sending the data to the remote device.  
                int bytesSent = client.EndSend(ar);
                Console.WriteLine("Client: Sent {0} bytes to server.", bytesSent);

                // Signal that all bytes have been sent.  
                item.SendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Debug.WriteLine(e.ToString());
            }
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                SocketHelper item = (SocketHelper)ar.AsyncState;
                Socket client = (Socket)item.Socket;

                // Complete the connection.  
                client.EndConnect(ar);

                string debug = $"Client: Socket connected to {client.RemoteEndPoint.ToString()}";

                Console.WriteLine(debug);
                Debug.WriteLine(debug);

                // Signal that the connection has been made.  
                item.ConnectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Debug.WriteLine($"Client: ConnectCallback Error: {e.ToString()}");
            }
        }
        #endregion

    }
}
