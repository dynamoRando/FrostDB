using Harness.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Harness.Base
{
    public class Mode : IMode
    {
        public Mode(App app)
        {
            App = app;
        }

        public App App { get; set; }

        public void CreateNewDb()
        {
            string result = string.Empty;
            string dbName = string.Empty;

            while (result != "y")
            {
                dbName = App.Prompt("enter db name:");
                result = App.Prompt($"db will be named {dbName} - (y) to confirm, otherwise no");
            }

            App.Process.AddDatabase(dbName);

            App.Write($"Db named {dbName} created");
        }
    }
}
