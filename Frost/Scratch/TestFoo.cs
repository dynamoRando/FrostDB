using System;
using System.Collections.Generic;
using FrostDB.Instance;
using FrostDB.DataStore;
using System.Text;
using FrostDB.Base;
using FrostDB.Instance.Database;
using FrostDB.Interface;

namespace FrostDB.Scratch
{
    public class TestFoo
    {
        public void Foo()
        {
            var host = new Host();

            var testName1 = "foo";
            var testName2 = "bar";

            host.AddDatabase(testName1);
            host.AddDatabase(testName2);

            Database hostDatabase = (Database)host.GetDatabase(testName1);

            var columns = new List<Column>();

            // if ITable had List<IColumn> instead of List<Column>, we would've had to do this --
            //var regularTable = new Table("test", columns.ConvertAll(t => (IColumn)t));
            
            var regularTable = new Table("test", columns, hostDatabase);
            var coopTable = new CooperativeTable("test", columns, hostDatabase);

            hostDatabase.AddTable(coopTable);
            hostDatabase.AddTable(regularTable);

            var store = new Store();
        }
    }
}
