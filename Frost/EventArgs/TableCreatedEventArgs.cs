using FrostDB.Base;
using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.EventArgs
{
    public class TableCreatedEventArgs : System.EventArgs, IEventArgs
    {
        public IDatabase Database { get; set; }
        public ITable<Column, Row> Table { get; set; }
    }
}
