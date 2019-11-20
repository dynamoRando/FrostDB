using FrostDB.Interface;
using Harness;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using FrostDB;
using Harness.Interface;

namespace Harness.Modes
{
    public class DbMode : BaseMode
    {
        #region Private Fields
        private bool _stayInMode = false;
        private TableMode _tableMode;
        private ParticipantMode _participantMode;
        #endregion

        #region Public Properties
        public Database Database { get; set; }
        public PartialDatabase PartialDatabase { get; set; }
        public string DatabaseName => (Database is null) ? string.Empty : Database.Name;
        public string PartialDatabaseName =>
            (PartialDatabase is null) ? string.Empty : PartialDatabase.Name;
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
            IDatabase db = null;

            do
            {
                if (Database is null)
                {
                    db = UseDb();
                }

                var result = this.Prompt("Would you like to (s)witch dbs, or take (a)ction on db?");

                switch (result)
                {
                    case "s":
                        db = UseDb();
                        break;
                    case "a":
                        if (db is Database)
                        {
                            PerformActionOnDb();
                        }
                        else if (db is PartialDatabase)
                        {
                            PerformActionOnPartialDb();
                        }
                        break;
                }
            } while (_stayInMode);
        }

        public void PerformActionOnPartialDb()
        {

        }

        public void PerformActionOnDb()
        {
            var result = this.Prompt("Specify action: (v)iew tables, (m)odify tables, (ap) - add participant, (l)eave prompt");
            switch (result)
            {
                case "l":
                    return;
                case "m":
                    _tableMode = new TableMode(App, Database);
                    _tableMode.Prompt();
                    break;
                case "v":
                    _tableMode = new TableMode(App, Database);
                    _tableMode.ShowTables();
                    break;
                case "ap":
                    _participantMode = new ParticipantMode(App, Database);
                    _participantMode.Prompt();
                    break;
            }
        }

        public IDatabase UseDb()
        {
            IDatabase database = null;

            App.Write("Listing databases. Use (em) to exit mode.");

            var db = Process.GetDatabases();
            var pdb = Process.GetPartialDatabasesString();

            App.Write("Databases: ");
            db.ForEach(d => { App.Write(d); });

            App.Write("Partial Databases: ");
            pdb.ForEach(k => { App.Write(k); });

            var dbName = this.Prompt("Enter db name to use: ");

            if (db.Contains(dbName))
            {
                Database = Process.GetFullDatabase(dbName);
                PartialDatabase = null;

                App.Write($"Using Db {DatabaseName}");

                return Database;
            }
            else if (pdb.Contains(dbName))
            {
                PartialDatabase = Process.GetPartialDatabase(dbName);
                this.Database = null;

                App.Write($"Using Db {PartialDatabaseName}");

                return PartialDatabase;
            }
            else
            {
                App.Write("unknown db name");
            }

            return database;
        }
        #endregion

        #region Private Methods
        private string Prompt(string message)
        {
            Console.WriteLine(message);
            Console.Write($"[Mode -> Database: ({((DatabaseName == string.Empty) ? PartialDatabaseName : DatabaseName)})] [(em) to exit mode] ==>");

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
