using FrostDB.Interface;
using Harness.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using FrostDB.Base;

namespace Harness.Modes
{
    public class DbMode : BaseMode
    {
        #region Private Fields
        private bool _stayInMode = false;
        #endregion

        #region Public Properties
        public IDatabase Database { get; set; }
        public string DatabaseName => (Database is null) ? string.Empty : Database.Name;
        #endregion

        #region Events
        #endregion

        #region Constructors
        public DbMode(App app) : base(app) { }
        #endregion

        #region Public Methods
        public override void Prompt()
        {
            _stayInMode = true;

            while (_stayInMode)
            {
                if (Database is null)
                {
                    UseDb();
                }

                var result = this.Prompt("Would you like to (s)witch dbs, or take (a)ction on db?");

                switch (result)
                {
                    case "s":
                        UseDb();
                        break;
                    case "a":
                        PerformActionOnDb();
                        break;
                }
            }
        }

        public void PerformActionOnDb()
        {
            this.Prompt("PerformActionOnDb not filled out");
        }

        public void UseDb()
        {
            App.Write("Listing databases. Use (em) to exit mode.");

            var db = Process.GetDatabases();
            var pdb = Process.GetPartialDatabases();

            App.Write("Databases: ");
            db.ForEach(d => { App.Write(d); });

            App.Write("Partial Databases: ");
            pdb.ForEach(k => { App.Write(k); });

            var dbName = this.Prompt("Enter db name to use: ");

            if (db.Contains(dbName))
            {
                Database = Process.GetDatabase(dbName);
                App.Write($"Using Db {DatabaseName}");
            }
            else if (pdb.Contains(dbName))
            {
                Database = Process.GetPartialDatabase(dbName);
                App.Write($"Using Db {DatabaseName}");
            }
            else
            {
                App.Write("unknown db name");
            }
        }
        #endregion

        #region Private Methods
        private string Prompt(string message)
        {
            Console.WriteLine(message);
            Console.Write($"[DbMode ({DatabaseName})] (em) to exit ==>");

            var _consoleLine = Console.ReadLine();

            if (_consoleLine == "exit")
            {
                App.Write("Quitting...");
                App.Quit();
            }

            if (_consoleLine == "em")
            {
                App.Write("Quitting DbMode...");
                _stayInMode = false;
            }

            return _consoleLine;
        }
        #endregion
    }
}
