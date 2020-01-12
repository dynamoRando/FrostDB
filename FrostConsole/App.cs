using System;
using System.Collections.Generic;
using System.Text;
using FrostDB;
using System.Threading.Tasks;

namespace FrostConsole
{
    public static class App
    {
        #region Private Fields
        static bool keepRunning = true;
        #endregion

        #region Public Properties
        public static Process Process { get; set; }
        public static bool IsRunning => keepRunning;
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        public static void Configure()
        {
            var ipAddress = Prompt("Enter IP Address");
            var portNumber = Prompt("Enter Data PortNumber (default 516)");
            var consolePortNumber = Prompt("Enter Data PortNumber (default 519)");

            Console.WriteLine($"IP Address: {ipAddress} and PortNumber: {portNumber} and ConsolePort {consolePortNumber} - correct y/n?");
            var result = Console.ReadLine();
            if (result == "y")
            {
                Process = new Process(ipAddress, Convert.ToInt32(portNumber), Convert.ToInt32(consolePortNumber));
                Process.LoadDatabases();
                Process.StartRemoteServer();
                Process.StartConsoleServer();
                keepRunning = true;
                OutputProcessInfo();
            }

        }
        public static void Startup()
        {
            if (Process is null)
            {
                Process = new Process();
                Process.LoadDatabases();
                Process.StartRemoteServer();
                Process.StartConsoleServer();
                keepRunning = true;
                OutputProcessInfo();
            }
        }
        public static void Shutdown()
        {
            if (Process != null)
            {
                Process.StopConsoleServer();
                Process.StopRemoteServer();
                keepRunning = false;
            }
        }
        public static string Prompt(string message)
        {
            Console.WriteLine(message);
            Console.Write("=>");
            return Console.ReadLine();
        }
        #endregion

        #region Private Methods
        private static void ConfigureInstance()
        {
            if (Process is null)
            {
                Process = new Process();
            }
        }
        private static void OutputProcessInfo()
        {
            Console.WriteLine("==========================");
            Console.WriteLine("FrostDb Process Information");
            Console.WriteLine($"Id: {Process.Id }");
            Console.WriteLine($"Ip Address: {Process.Configuration.Address.ToString() }");
            Console.WriteLine($"Data Port Number: {Process.Configuration.DataServerPort.ToString() }");
            Console.WriteLine($"Console Port Number: {Process.Configuration.ConsoleServerPort.ToString() }");
            Console.WriteLine($"Database Folder: {Process.Configuration.DatabaseFolder }");
            Console.WriteLine($"Contract Folder: {Process.Configuration.ContractFolder}");
            Console.WriteLine("==========================");
        }
        #endregion


    }
}
