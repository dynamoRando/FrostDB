using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.EventArgs
{
    public class RowModifiedEventArgs : System.EventArgs, IEventArgs
    {
        public IDatabase Database { get; set; }
        public Table Table { get; set; }
        public Guid? RowId { get; set; }
    }
}
