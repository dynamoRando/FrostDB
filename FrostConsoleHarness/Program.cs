using System;
using System.Collections;
using System.Collections.Generic;
using FrostDB;

namespace FrostConsoleHarness
{
    class Program
    {
        static FrostInstanceManager Manager = new FrostInstanceManager();
        static ProcessConfigurator Configurator = new ProcessConfigurator();

        static void Main(string[] args)
        {
            string input = string.Empty;

            do
            {
                input = ShowMenu();
                EvaluateInput(input);
            }
            while (input != "e");
        }

        static string ShowMenu()
        {
            Console.WriteLine("Frost Console Harness");
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("(s) - start a new instance with default settings");
            Console.WriteLine("(l) - list all running instances");
            Console.WriteLine("(lh) - load an existing harness");
            Console.WriteLine("(c) - configure a new instance");
            Console.WriteLine("(k) - kill a existing instance");
            Console.WriteLine("(e) - exit application");

            return Console.ReadLine();
        }

        static void EvaluateInput(string input)
        {
            switch (input)
            {
                case "s":
                    StartDefaultProcess();
                    break;
                case "c":
                    ConfigureNewProcess();
                    break;
                case "k":
                    KillExistingProcess();
                    break;
                case "l":
                    ListInstances();
                    break;
                case "lh":
                    LoadExistingHarness();
                    break;
                default:
                    // do nothing
                    break;
            }
        }

        static void StartDefaultProcess()
        {
            Console.WriteLine("Start A New Default Process");
            throw new NotImplementedException();
        }

        static void ListInstances()
        {
            Manager.ListRunningInstances();
        }

        static void ConfigureNewProcess()
        {
            Console.WriteLine("Configure A New Process");
            var item = Configurator.ConfigureInstance();
            if (item != null)
            {
                Manager.AddInstance(item);
                Console.WriteLine("Instance added");
            }
        }

        static void KillExistingProcess()
        {
            Console.WriteLine("Kill an existing process");
            throw new NotImplementedException();
        }

        static void LoadExistingHarness()
        {
            Configurator.LoadExistingHarness();
        }

    }
}
