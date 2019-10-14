using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
{
    public class EventName
    {
        public class Database
        {
            public const string Created = "Database_Created";
        }

        public class Table
        {
            public const string Created = "Table_Created";
            public const string Dropped = "Table_Dropped";
        }
    }
    
}
