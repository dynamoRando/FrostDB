using FrostCommon.ConsoleMessages;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace FrostCommon.Net
{
    public class SocketHelper
    {
        public Socket Socket { get; set; }
        public Location Location { get; set; }
        public ManualResetEvent ConnectDone = new ManualResetEvent(false);
        public ManualResetEvent SendDone = new ManualResetEvent(false);
        public int TimeOut;
        public ManualResetEvent ReceiveDone = new ManualResetEvent(false);
        public ManualResetEvent DisconnectDone = new ManualResetEvent(false);
        public StateObject StateObject { get; set; }

        public SocketHelper(Socket socket, Location location, int timeout)
        {
            Socket = socket;
            Location = location;
            TimeOut = timeout;
        }

        public SocketHelper(Location location)
        {
            Location = location;
        }
    }
}
