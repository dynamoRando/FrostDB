using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
{
    public static class EventName
    {
        public static EventNameTable Table { get { return new EventNameTable(); } }
        public static EventNameDatabase Database { get { return new EventNameDatabase(); } }
    }

    public class EventNameDatabase
    {
        public string Created => "Database_Created";
    }

    public class EventNameTable
    {
        public string Created => "Table_Created";
        public string Dropped => "Table_Dropped";
    }
}
