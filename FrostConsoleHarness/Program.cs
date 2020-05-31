using System;
using System.Collections;
using System.Collections.Generic;
using FrostDb;

namespace FrostConsoleHarness
{
    class Program
    {
        static List<Process> FrostInstances = new List<Process>();

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
            Console.WriteLine("(s) - start a new instance");
            Console.WriteLine("(c) - configure a new instance");
            Console.WriteLine("(e) - exit application");

            return Console.ReadLine();
        }

        static string EvaluateInput(string input)
        {
            
        }
    }
}
