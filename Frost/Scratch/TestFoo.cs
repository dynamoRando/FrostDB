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

            var database = new Database("foo");
            var hostDatabase = new HostDatabase("bar");

            var columns = new List<Column>();

            var regularTable = new Table("test", columns);
            var coopTable = new CooperativeTable("test", columns);

            hostDatabase.AddTable(coopTable);
            hostDatabase.AddTable(regularTable);

            var store = new Store();
        }
    }
}
