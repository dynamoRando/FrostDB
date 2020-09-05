using System;
using FrostDB;
using System.Runtime.Serialization;

namespace FrostConsoleHarness
{
    [Serializable]
    class FrostInstance : ISerializable
    {
        public FrostInstance()
        {
        }

        public string IPAddress { get; set; }
        public int PortNumber { get; set; }
        public int ConsolePortNumber { get; set; }
        public Process Instance { get; set; }
        public string RootDirectory { get; set; }

        protected FrostInstance(SerializationInfo info, StreamingContext context)
        {
            IPAddress = info.GetString("IPAddress");
            PortNumber = info.GetInt32("PortNumber");
            ConsolePortNumber = info.GetInt32("ConsolePortNumber");
            RootDirectory = info.GetString("RootDirectory");
            SetupNewInstance();
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("IPAddress", IPAddress);
            info.AddValue("PortNumber", PortNumber);
            info.AddValue("ConsolePortNumber", ConsolePortNumber);
            info.AddValue("RootDirectory", RootDirectory);
        }

        private void SetupNewInstance()
        {
            if (this.Instance == null)
            {
                Instance = new Process(IPAddress, PortNumber, ConsolePortNumber, RootDirectory);
                Instance.LoadDatabases();
                Instance.LoadPartialDatabases();
                Instance.StartRemoteServer();
                Instance.StartConsoleServer();

                Instance.LoadDatabases2();
            }
        }
    }
}