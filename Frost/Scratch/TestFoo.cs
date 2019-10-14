using FrostDB.Base;
using FrostDB.EventArgs;
using FrostDB.Interface;
using System;
using System.Collections.Generic;

namespace FrostDB.Scratch
{
    public class TestFoo
    {
        public void Foo()
        {
            var process = new Process();

            var testName1 = "foo";
            var testName2 = "bar";

            process.AddDatabase(testName1);
            process.AddDatabase(testName2);

            var regularDB = process.GetDatabase(testName1);
            
            var columns = new List<Column>();

            // if ITable had List<IColumn> instead of List<Column>, we would've had to do this --
            //var regularTable = new Table("test", columns.ConvertAll(t => (IColumn)t));
            
            var regularTable = new Table("test", columns, regularDB);
            var virtualTable = new VirtualTable("test", columns, regularDB);

            regularDB.AddTable(virtualTable);
            regularDB.AddTable(regularTable);

            var remoteRow = virtualTable.GetNewRow();
            var row = virtualTable.GetRow(new Row());
        }

        public void TestEvent()
        {
            //var listener = new Action<IEventArgs>(TableCreatedEventTest);
            EventManager.StartListening(EventName.Table.Created, new Action<IEventArgs>(TableCreatedEventTest));
        }

        public void TableCreatedEventTest(IEventArgs e)
        {
            if (e is TableCreatedEventArgs)
            {
                var args = (TableCreatedEventArgs)e;
                // handle table created event
                Console.WriteLine(args.Table.Name);
                Console.WriteLine(args.Database.Name);
            }

            throw new NotImplementedException();
        }
    }
}
