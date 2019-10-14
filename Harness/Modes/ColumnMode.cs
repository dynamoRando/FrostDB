using FrostDB.Base;
using Harness.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Harness.Modes
{
    public class ColumnMode : BaseMode
    {
        #region Private Fields
        #endregion

        #region Public Properties

        #endregion

        #region Events
        #endregion

        #region Constructors
        public ColumnMode(App app) : base(app) { }
        #endregion

        #region Public Methods
        public static List<Column> CreateColumnsForTable(App app, string tableName, string databaseName)
        {
            var columns = new List<Column>();
            string result;

            do
            {
                var message = "Would you like to add a column? (y), otherwise will return";
                result = Prompt(app, message, tableName, databaseName);

                if (result != "y")
                {
                    break;
                }

                var column = new Column(
                    PromptForColumnName(app, tableName, databaseName),
                    PromptForDataType(app, tableName, databaseName));

                columns.Add(column);
            }
            while (result == "y");

            return columns;
        }
        public override void Prompt()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        private static Type PromptForDataType(App app, string tableName, string databaseName)
        {
            var message = "Enter a data type: (i)nt, (f)loat, (d)ateTime, (s)tring";
            var result = Prompt(app, message, tableName, databaseName);

            message = $"DataType is {result}, correct? (y), otherwise will repeat";
            var confirm = Prompt(app, message, tableName, databaseName);

            if (confirm == "y")
            {
                switch (result)
                {
                    case "i":
                        return Type.GetType("System.Int32");
                    case "s":
                        return Type.GetType("System.String");
                    case "d":
                        return Type.GetType("System.DateTime");
                    case "f":
                        return Type.GetType("System.Single");
                }
            }
            else
            {
                PromptForDataType(app, tableName, databaseName);
            }

            return null;
        }

        private static string PromptForColumnName(App app, string tableName, string databaseName)
        {
            var message = "Enter a column name: ";
            var result = Prompt(app, message, tableName, databaseName);

            message = $"Column Name is {result}, correct? (y), otherwise will repeat";

            var confirm = Prompt(app, message, tableName, databaseName);

            if (confirm == "y")
            {
                return result;
            }
            else
            {
                PromptForColumnName(app, tableName, databaseName);
            }

            return string.Empty;
        }

        private static string Prompt(App app, string message, string tableName, string databaseName)
        {
            Console.WriteLine(message);
            Console.Write($"[Mode -> Column | Table: ({tableName}) | Database: ({databaseName})] [(em) to exit mode] ==>");

            var _consoleLine = Console.ReadLine();

            if (_consoleLine == "exit")
            {
                app.Write("Quitting...");
                app.Quit();
            }

            if (_consoleLine == "em")
            {
                app.Write("Quitting ColumnMode...");
                return string.Empty;
            }

            return _consoleLine;
        }
        #endregion
    }
}
