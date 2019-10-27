using FrostDB.Base;
using FrostDB.Interface;
using Harness.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Harness.Modes
{
    public class TableMode : BaseMode
    {
        #region Private Fields
        private bool _stayInMode = false;
        #endregion

        #region Public Properties
        public Database Database { get; }
        public PartialDatabase PartialDatabase { get; }
        public ITable<Column, IRow> Table { get; }
        public string TableName => (Table is null) ? string.Empty : Table.Name;
        public string DatabaseName => (Database is null) ? string.Empty : Database.Name;
        public string PartialDatabaseName => (PartialDatabase is null) ? string.Empty : PartialDatabase.Name;
        #endregion

        #region Events
        #endregion

        #region Constructors
        public TableMode(App app, Database db) : base(app) 
        {
            Database = db;
        }

        public TableMode(App app, PartialDatabase db) : base(app) 
        {
            PartialDatabase = db;
        }

        #endregion

        #region Public Methods
        public override void Prompt()
        {
            _stayInMode = true;

            while (_stayInMode)
            {
                var result = this.Prompt("(a) - add regular table, " +
                    "(av) - add virtual table, " +
                    "(em) to exit");

                switch (result)
                {
                    case "em":
                        _stayInMode = false;
                        break;
                    case "a":
                        AddTable();
                        break;
                    case "av":
                        AddVirtualTable();
                        break;
                }
            }
        }

        public void ShowTables()
        {
            Database.Tables.ForEach(t => 
            {
                Console.WriteLine($"Table: {t.Name}");

                t.Columns.ForEach(c => 
                {
                    Console.WriteLine($"Column: {c.Name}");
                    Console.WriteLine($"DataType: {c.DataType.ToString()}");
                });

            });
        }
        #endregion

        #region Private Methods
        private void AddVirtualTable()
        {
            var result = this.Prompt("Specify table name, (q)uit to exit this prompt: ");

            if (result == "q")
            {
                return;
            }

            var confirm = this.Prompt($"table will be named {result}, is this correct? (y), " +
                $"otherwise will prompt again");

            if (confirm == "y")
            {
                if (PartialDatabase is null)
                {
                    var table = new VirtualTable(result, ColumnMode.CreateColumnsForTable(App, result, DatabaseName), Database);
                    Database.AddTable(table);
                    App.Write($"{Database.Name} added table {table.Name}");
                }
            }
            else
            {
                AddVirtualTable();
            }
        }
        private void AddTable()
        {
            var result = this.Prompt("Specify table name, (q)uit to exit this prompt: ");
            
            if (result == "q")
            {
                return;
            }

            var confirm = this.Prompt($"Table will be named {result}, is this correct? (y), " +
                $"otherwise will prompt again");

            if (confirm == "y")
            {
                if (Database is null)
                {
                    var table = new Table(result, ColumnMode.CreateColumnsForTable(App, result, PartialDatabaseName), PartialDatabase);
                    PartialDatabase.AddTable(table);
                    App.Write($"{PartialDatabase.Name} added table {table.Name}");
                }

                if (PartialDatabase is null)
                {
                    var table = new Table(result, ColumnMode.CreateColumnsForTable(App, result, DatabaseName), Database);
                    Database.AddTable(table);
                    App.Write($"{Database.Name} added table {table.Name}");
                }   
            }
            else
            {
                AddTable();
            }
        }

        private string Prompt(string message)
        {
            Console.WriteLine(message);
            Console.Write($"[Mode -> Table: ({TableName}) | Database: ({((DatabaseName == string.Empty) ? PartialDatabaseName : DatabaseName)})] [(em) to exit mode] ==>");

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
