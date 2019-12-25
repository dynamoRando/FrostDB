using System;
using System.Collections.Generic;
using System.Text;

namespace FrostCommon
{
    public class MessageConsoleAction
    {
        public class Process
        {
            public const string Get_Databases = "Process.Get_Databases";
            public const string Get_Databases_Response = "Process.Get_Databases_Response";

            public const string Get_Id = "Process.Get_Id";
            public const string Get_Id_Response = "Process.Get_Id_Response";

            public const string Add_Database = "Process.Add_Database";
            public const string Add_Database_Response = "Process.Add_Database_Response";

            public const string Remove_Datababase = "Process.Remove_Database";
            public const string Remove_Database_Response = "Process.Remove_Database_Response";
        }

        public class Database
        {
            public const string Get_Database_Info = "Database.Get_Info";
            public const string Get_Database_Info_Response = "Database.Get_Info_Response";
            public const string Get_Database_Tables = "Database.Get_Tables";
            public const string Get_Database_Tables_Response = "Database.Get_Tables_Response";
        }

        public class Table
        {
            public const string Get_Table_Info = "Table.Get_Info";
            public const string Get_Table_Info_Response = "Table.Get_Info_Response";
        }
    }
}
