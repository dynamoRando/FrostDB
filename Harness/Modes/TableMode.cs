using FrostDB;
using FrostDB.Interface;
using Harness;
using System;
using System.Collections.Generic;
using System.Text;

namespace Harness.Modes
{
    public class TableMode : BaseMode
    {
        #region Private Fields
        private bool _stayInMode = false;
        private Table _table;
        #endregion

        #region Public Properties
        public Database Database { get; }
        public PartialDatabase PartialDatabase { get; }
        public Table Table => _table;
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

            do
            {
                var result = this.Prompt("(a) - add regular table, " +
                    "(av) - add virtual table, " +
                    "(ar) - add record to table, " +
                    "(fr) - find record on table " +
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
                    case "ar":
                        AddRecordToTable();
                        break;
                    case "fr":
                        FindRecords();
                        break;
                }
            } while (_stayInMode);
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
        private void FindRecords()
        {
            PromptAndSetTable("Enter a table to search records for:");
            var query = this.Prompt("Enter your query string:");
            var rows = _table.GetRows(query);

            var tableString = "";
            var rowStrings = new List<string>();

            App.Write($"TableName: { _table.Name}");
            App.Write($"Query: {query}");

            if (rows.Count > 0)
            {
                var r = rows[0];

                r.ColumnIds.ForEach(column =>
                {
                    var x = _table.GetColumn(column);

                    tableString += $"| {x.Name} ";
                });

                App.Write(tableString);

                rows.ForEach(row =>
                {
                    var rowString = "";

                    row.Values.ForEach(value =>
                    {
                        rowString += $"| {Convert.ChangeType(value.Value, value.ColumnType).ToString()} ";
                    });

                    rowStrings.Add(rowString);
                });

                rowStrings.ForEach(str => App.Write(str));
            }
        }
        private bool IsPartialDatabase()
        {
            if (this.PartialDatabase is null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void PromptAndSetTable(string prompt)
        {
            ShowTables();

            var tableName = this.Prompt(prompt);

            if (!IsPartialDatabase())
            {
                if (Database.HasTable(tableName))
                {
                    var table = Database.GetTable(tableName);
                    _table = table;
                }
            }
        }

        private void AddRecordToTable()
        {
            var prompt = "Specify table name to add record to:";
            PromptAndSetTable(prompt);

            if (!(_table is null))
            {
                var form = _table.GetNewRow(Database.Id);

                form.Row.ColumnIds.ForEach(c =>
                {
                    var x = _table.GetColumn(c);
                    var val = this.Prompt($"For column " +
                        $"{x.Name} for type " +
                        $"{x.DataType.ToString()} " +
                        $"Please enter a value: ");

                    form.Row.AddValue(c,
                        Convert.ChangeType(val, x.DataType), x.Name, x.DataType);

                });

                this.Prompt("Adding row");
                _table.AddRow(form);
            }
        }
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
                    var table = new Table(result, ColumnMode.CreateColumnsForTable(App, result, DatabaseName), Database.Id);
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
                    var table = new Table(result,
                        ColumnMode.CreateColumnsForTable(App, result, PartialDatabaseName), PartialDatabase.Id);
                    PartialDatabase.AddTable(table);
                    App.Write($"{PartialDatabase.Name} added table {table.Name}");
                }

                if (PartialDatabase is null)
                {
                    var table = new Table(result, ColumnMode.CreateColumnsForTable(App, result, DatabaseName), Database.Id);
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
