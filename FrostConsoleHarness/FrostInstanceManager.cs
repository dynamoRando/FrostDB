using System;
using System.Collections;
using System.Collections.Generic;
using FrostDB;
using System.Linq;

namespace FrostConsoleHarness
{
    class FrostInstanceManager
    {
        public List<FrostInstance> Processes = new List<FrostInstance>();

        public void AddInstance(FrostInstance instance)
        {
            if (!ContainsInstance(instance))
            {
                Processes.Add(instance);
            }
        }

        public bool ContainsInstance(FrostInstance instance)
        {
            return (Processes.Any(i => i.IPAddress == instance.IPAddress) && Processes.Any(j => j.PortNumber == instance.PortNumber)
            && Processes.Any(k => k.ConsolePortNumber == instance.ConsolePortNumber));
        }

        public void ListRunningInstances()
        {
            Processes.ForEach(i => 
            {
                Console.WriteLine($"IP Address: { i.IPAddress } " +
                $"PortNumber: { i.PortNumber.ToString() } ConsolePortNumber: { i.ConsolePortNumber.ToString() }");
            });
        }
    }
}