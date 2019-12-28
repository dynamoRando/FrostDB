using System;
using System.Collections.Generic;
using System.Text;

namespace FrostCommon
{
    public static class MessageConsoleAction
    {
        public static class Process
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

        public static class Database
        {
            public const string Get_Database_Info = "Database.Get_Info";
            public const string Get_Database_Info_Response = "Database.Get_Info_Response";
            public const string Get_Database_Tables = "Database.Get_Tables";
            public const string Get_Database_Tables_Response = "Database.Get_Tables_Response";
            public const string Add_Table_To_Database = "Database.Add_New_Table";
            public const string Add_Table_To_Database_Response = "Database.Add_New_Table_Response";
            public const string Remove_Table_From_Database = "Database.Remove_Table";
            public const string Remove_Table_From_Datababase_Response = "Database.Remove_Table_Response";
        }

        public static class Table
        {
            public const string Get_Table_Info = "Table.Get_Info";
            public const string Get_Table_Info_Response = "Table.Get_Info_Response";

            public const string Add_Column = "Table.Add_Column";
            public const string Add_Column_Response = "Table.Add_Column_Response";

            public const string Remove_Column = "Table.Remove_Column";
            public const string Remove_Column_Response = "Table.Remove_Column_Response";
        }
    }
}
