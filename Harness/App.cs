using FrostDB.Base;
using Harness.Base;
using Harness.Interface;
using Harness.Modes;
using System;


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

        public void Quit()
        {
            _running = false;
        }

        public IMode PromptForMode()
        {
            IMode mode = null;

            Write("Specify action: " +
                "(c) - create a db, " +
                "(u) - use db, " +
                "(i) - output process info " +
                "or (exit)") ;

            switch (Prompt())
            {
                case "c":
                    mode = new MainMode(this);
                    break;
                case "u":
                    mode = new DbMode(this);
                    break;
                case "i":
                    OutputProcessInfo();
                    break;
            }

            return mode;
        }

        public void PromptForStartup()
        {
            Write("Specify action: (s)tart, (exit) to quit");

            var totalDBs = 0;

            switch (Prompt())
            {
                case "s":
                    Write("Starting app...");
                    _process = new FrostDB.Base.Process();
                    totalDBs = _process.LoadDatabases();
                    break;
                default:
                    Write("Unknown startup");
                    break;
            }

            Write("Total DBs loaded: " + totalDBs.ToString());

            OutputProcessInfo();
        }

        public void OutputProcessInfo()
        {
            Console.WriteLine($"Process Named: {_process.Name} with Id: {_process.Id}");
            Console.WriteLine($"Process IP Address: {_process.Configuration.Address} with Port: {_process.Configuration.ServerPort}");
            Console.WriteLine($"Total DBs: { _process.Databases.Count.ToString()}");
            Console.WriteLine($"Total partial DBs: { _process.PartialDatabases.Count.ToString()}");
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
        public void Write(string value)
        {
            Console.Write(value);
            Console.WriteLine();
        }
        #endregion

        #region Private Methods
        #endregion


    }
}
