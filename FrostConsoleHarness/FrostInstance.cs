using System;
using FrostDB;

namespace FrostConsoleHarness
{
    [Serializable]
    class FrostInstance
    {
        public string IPAddress { get; set; }
        public int PortNumber { get; set; }
        public int ConsolePortNumber { get; set; }
        public Process Instance { get; set; }
        public string RootDirectory {get; set;}
    }
}