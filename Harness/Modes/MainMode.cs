using Harness.Base;
using Harness.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Harness.Modes
{
    public class MainMode : BaseMode
    {
        #region Private Fields
        private bool _stayInMode = false;
        #endregion

        #region Public Properties
        #endregion

        #region Events
        #endregion

        #region Constructors
        public MainMode(App app) : base(app)
        {
            App = app;
        }
        #endregion

        #region Public Methods
        public override void Prompt()
        {
            _stayInMode = true;

            do
            {
                var result = Prompt("Would you like to create a (db) or partial (pdb)? " +
             "Use (em) to exit mode");

                switch (result)
                {
                    case "db":
                        CreateNewDb();
                        break;
                    case "pdb":
                        CreateNewPartialDb();
                        break;
                }
            } while (_stayInMode);
        }

        public void CreateNewDb()
        {
            string result = string.Empty;
            string dbName = string.Empty;

            while (result != "y")
            {
                dbName = this.Prompt("enter db name:");
                result = this.Prompt($"db will be named {dbName} - (y) to confirm, otherwise no");
            }

            App.Process.AddDatabase(dbName);

            App.Write($"Db named {dbName} created");
        }

        public void CreateNewPartialDb()
        {
            string result = string.Empty;
            string dbName = string.Empty;

            while (result != "y")
            {
                dbName = this.Prompt("enter partial db name:");
                result = this.Prompt($"partial db will be named {dbName} - (y) to confirm, otherwise no");
            }

            App.Process.AddPartialDatabase(dbName);

            App.Write($"Db named {dbName} created");
        }
        #endregion

        #region Private Methods
        private string Prompt(string message)
        {
            Console.WriteLine(message);
            Console.Write("[MainMode] (em) to exit ==>");
            var _consoleLine = Console.ReadLine();

            if (_consoleLine == "exit")
            {
                App.Write("Quitting...");
                App.Quit();
            }

            if (_consoleLine == "em")
            {
                App.Write("Quitting MainMode...");
                _stayInMode = false;
            }

            return _consoleLine;
        }
        #endregion
    }
}
