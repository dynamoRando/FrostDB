namespace FrostDB.Base
{
    public class EventName
    {
        public class Database
        {
            public const string Created = "Database_Created";
            public const string Deleted = "Database_Deleted";
        }

        public class Participant
        {
            public const string Added = "Participant_Added";
            public const string Removed = "Participant_Removed";
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
            public const string Read = "Row_Read";

            public const string Added_Remotely = "Row_Added_Remotely";
            public const string Modified_Remotely = "Row_Modified_Remotely";
            public const string Deleted_Remotely = "Row_Deleted_Remotely";
            public const string Read_Remotely = "Row_Read_Remotely";
        }

        public class Columm
        {
            public const string Added = "Column_Added";
            public const string Deleted = "Column_Deleted";
            public const string DataTypeChanged = "Column_DataTypeChanged";
        }
    }
    
}
