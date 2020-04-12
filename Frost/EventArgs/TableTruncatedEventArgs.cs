using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.EventArgs
{
    public class TableTruncatedEventArgs : System.EventArgs, IEventArgs
    {
        public IDatabase Database { get; set; }
        public Table Table { get; set; }
    }
}
