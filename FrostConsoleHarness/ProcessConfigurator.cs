using System;
using FrostDB;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft;

namespace FrostConsoleHarness
{
    class ProcessConfigurator
    {
        public List<FrostInstance> LoadExistingHarness()
        {
            var harnessLocation = Prompt.For("Enter config location:");
            List<FrostInstance> item = new List<FrostInstance>();
            if (File.Exists(harnessLocation))
            {
                    var fileText = File.ReadAllText(harnessLocation);
                    item = JsonConvert.DeseralizeObject<HarnessFile>(fileText).Instances;
            }

            return item;
        }

        public void SaveCurrentHarness(List<FrostInstance> instances, string fileLocation)
        {
            HarnessFile file = new HarnessFile();
            file.Instances = instances;

            var item = JsonConvert.SeralizeObject(file);
            File.WriteAllText(fileLocation, item);
        }

        public FrostInstance ConfigureInstance()
        {
            Process process = null;
            FrostInstance instance = null;
            var ipAddress = Prompt.For("Enter IP Address");
            var portNumber = Prompt.For("Enter Data PortNumber (default 516)");
            var consolePortNumber = Prompt.For("Enter Data PortNumber (default 519)");
            var rootDirectory = Prompt.For("Enter root directory location");

            Console.WriteLine($"IP Address: {ipAddress} and PortNumber: {portNumber} and ConsolePort {consolePortNumber}  and dir: {rootDirectory} - correct y/n?");
            var result = Console.ReadLine();
            if (result == "y")
            {
                instance = new FrostInstance();
                instance.IPAddress = ipAddress;
                instance.PortNumber = Convert.ToInt32(portNumber);
                instance.ConsolePortNumber = Convert.ToInt32(consolePortNumber);
                instance.RootDirectory = rootDirectory;

                process = new Process(ipAddress, Convert.ToInt32(portNumber), Convert.ToInt32(consolePortNumber), rootDirectory);
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