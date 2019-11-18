using FrostDB;
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
