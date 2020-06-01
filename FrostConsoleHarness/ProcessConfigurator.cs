using System;
using FrostDB;

namespace FrostConsoleHarness
{
    class ProcessConfigurator
    {
        public FrostInstance ConfigureInstance()
        {
            Process process = null;
            FrostInstance instance = null;
            var ipAddress = Prompt.For("Enter IP Address");
            var portNumber = Prompt.For("Enter Data PortNumber (default 516)");
            var consolePortNumber = Prompt.For("Enter Data PortNumber (default 519)");

            Console.WriteLine($"IP Address: {ipAddress} and PortNumber: {portNumber} and ConsolePort {consolePortNumber} - correct y/n?");
            var result = Console.ReadLine();
            if (result == "y")
            {
                instance = new FrostInstance();
                instance.IPAddress = ipAddress;
                instance.PortNumber = Convert.ToInt32(portNumber);
                instance.ConsolePortNumber = Convert.ToInt32(consolePortNumber);

                process = new Process(ipAddress, Convert.ToInt32(portNumber), Convert.ToInt32(consolePortNumber));
                process.LoadDatabases();
                process.StartRemoteServer();
                process.StartConsoleServer();

                instance.Instance = process;

                return instance;
            }
            else
            {
                Console.WriteLine("Quitting without configuring instance");
                return null;
            }
        }
    }
}