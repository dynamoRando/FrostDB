using System;
using System.Collections.Generic;
using System.Text;

namespace FrostCommon
{
    public static class MessageConsoleAction
    {
        public static class Process
        {
            public const string Get_Databases = "Get_Databases";
            public const string Get_Databases_Response = "Get_Databases_Response";

            public const string Get_Id = "Get_Id";
            public const string Get_Id_Response = "Get_Id_Response";

            public const string Add_Database = "Add_Database";
            public const string Add_Database_Response = "Add_Database_Response";

            public const string Remove_Datababase = "Remove_Database";
            public const string Remove_Database_Response = "Remove_Database_Response";
        }

        public static class Database
        {
            public const string Get_Database_Info = "Get_Info";
            public const string Get_Database_Info_Response = "Get_Info_Response";
            public const string Get_Database_Tables = "Get_Tables";
            public const string Get_Database_Tables_Response = "Get_Tables_Response";
            public const string Add_Table_To_Database = "Add_New_Table";
            public const string Add_Table_To_Database_Response = "Add_New_Table_Response";
            public const string Remove_Table_From_Database = "Remove_Table";
            public const string Remove_Table_From_Datababase_Response = "Remove_Table_Response";
        }

        public static class Table
        {
            public const string Get_Table_Info = "Get_Info";
            public const string Get_Table_Info_Response = "Get_Info_Response";

            public const string Add_Column = "Add_Column";
            public const string Add_Column_Response = "Add_Column_Response";

            public const string Remove_Column = "Remove_Column";
            public const string Remove_Column_Response = "Remove_Column_Response";
        }
    }
}
