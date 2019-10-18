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
            public const string Deleted = "Database_Deleted";
        }

        public class Table
        {
            public const string Created = "Table_Created";
            public const string Dropped = "Table_Dropped";
        }
        
        public class Row
        {
            public const string Added = "Row_Added";
            public const string Modified = "Row_Modified";
            public const string Deleted = "Row_Deleted";
        }
    }
    
}
