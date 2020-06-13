using FrostDbClient;
using System;

namespace FrostBlazeServer.Services
{
    public class Client
    {
        public Client()
        {

        }
        
        public Client(string remoteIPAddress, string localIPAddress, int consolePortNumber, int studioPortNumber)
        {
            IPAddress = remoteIPAddress;
            ConsolePortNumber = consolePortNumber;
            StudioPortNumber = studioPortNumber;
            _client = new FrostClient(IPAddress, localIPAddress, consolePortNumber, studioPortNumber);
        }

        private FrostClient _client;
        public string IPAddress = string.Empty;
        public int ConsolePortNumber = 0;
        public int StudioPortNumber = 0;
        public FrostClient FrostClient
        {
            get
            {
                if (_client is null)
                {
                    return _client = new FrostClient(IPAddress, "127.0.0.1", ConsolePortNumber, StudioPortNumber);
                }
                else
                {
                    return _client;
                }
            }
            set
            {
                _client = value;
            }
        }
    }
}