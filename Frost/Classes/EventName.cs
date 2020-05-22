namespace FrostDB
{
    public class EventName
    {
        public class Message
        {
            public const string Message_Recieved = "Message_Recieved";
            public const string Message_Sent = "Message_Sent";
        }

        public class Contract
        {
            public const string Pending_Added = "Pending_Contract_Added";
            public const string Accepted = "Contract_Accepted";
            public const string Contract_Updated = "Contract_Updated";
        }
        public class Database
        {
            public const string Created = "Database_Created";
            public const string Deleted = "Database_Deleted";
        }

        public class Participant
        {
            public const string Added = "Participant_Added";
            public const string Removed = "Participant_Removed";
            public const string Pending = "Partcipant_Pending";
        }

        public class Table
        {
            public const string Created = "Table_Created";
            public const string Dropped = "Table_Dropped";

            public const string Truncated = "Table_Truncated";
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
