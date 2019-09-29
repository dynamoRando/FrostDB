using FrostDB.Base;
using System;
using System.Collections.Generic;
using System.Text;


namespace Harness
{
    public class App
    {
        #region Private Fields
        private string _consoleLine;
        private bool _running;
        private Process _process;
        #endregion

        #region Public Properties
        public string ConsoleLine { get => _consoleLine; set => _consoleLine = value; }
        public bool Running { get => _running; set => _running = value; }
        public Process Process { get => _process; set => _process = value; }
        #endregion

        #region Events
        #endregion

        #region Constructors
        public App()
        {
            _running = true;
        }
        #endregion

        #region Public Methods

        public void PromptForActions()
        {
            Write("Specify action: (c)reate db, (exit)");

            switch (Prompt())
            {
                case "c":
                    CreateNewDb();
                    break;
            }
        }

        public void PromptForMode()
        {
            Write("Specify which mode to run: (h)ost or (s)store, (exit) to quit");

            var totalDBs = 0;

            switch (Prompt())
            {
                case "h":
                    Write("Starting app in Host mode...");
                    _process = new FrostDB.Instance.Host();
                    totalDBs = _process.LoadDatabases();
                    break;
                case "s":
                    Write("Starting app in Store mode...");
                    _process = new FrostDB.DataStore.Store();
                    totalDBs = _process.LoadDatabases();
                    break;
            }

            Write("Total DBs loaded: " + totalDBs.ToString());

            OutputProcessInfo();
        }

        public void OutputProcessInfo()
        {
            Console.WriteLine($"Process Named: {_process.Name} with Id: {_process.Id}");
            Console.WriteLine($"Process IP Address: {_process.Configuration.Address} with Port: {_process.Configuration.ServerPort}");
        }

        public string Prompt(string message)
        {
            Console.WriteLine(message);
            Console.Write("==>");
            _consoleLine = Console.ReadLine();

            if (_consoleLine == "exit")
            {
                Write("Quitting...");
                _running = false;
            }

            return _consoleLine;
        }

        public string Prompt()
        {
            Console.Write("==>");
            _consoleLine = Console.ReadLine();

            if (_consoleLine == "exit")
            {
                Write("Quitting...");
                _running = false;
            }

            return _consoleLine;
        }
        #endregion

        #region Private Methods
        private void Write(string value)
        {
            Console.Write(value);
            Console.WriteLine();
        }

        private void CreateNewDb()
        {
            string result = string.Empty;
            string dbName = string.Empty;

            while (result != "y")
            {
                dbName = Prompt("enter db name:");
                result = Prompt($"db will be named {dbName} - (y) to confirm, otherwise no");
            }

            _process.AddDatabase(dbName);

            Write($"Db named {dbName} created");
        }

        #endregion


    }
}
