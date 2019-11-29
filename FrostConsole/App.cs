using System;
using System.Collections.Generic;
using System.Text;
using FrostDB;

namespace FrostConsole
{
    public static class App
    {
        #region Private Fields
        #endregion

        #region Public Properties
        public static Process Process { get; set; }
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

        }
        public static void Run()
        {
            if (Process is null)
            {
                Process = new Process();
                Process.LoadDatabases();
                Process.StartRemoteServer();
            }
        }
        public static void Shutdown()
        {

        }
        public static string Prompt(string message)
        {
            Console.WriteLine(message);
            Console.Write("=>");
            return Console.ReadLine();
        }
        #endregion

        #region Private Methods
        #endregion


    }
}
