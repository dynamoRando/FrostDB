using System;
using System.Collections.Generic;
using System.Text;

namespace FrostCommon
{
    public static class MessageConsoleAction
    {
        public static class Prompt
        {
            public const string Execute_Command = "Execute_Command";
            public const string Eecute_Command_Response = "Execute_Command_Response";
        }

        public static class Process
        {
            public const string Get_Databases = "Get_Databases";
            public const string Get_Databases_Response = "Get_Databases_Response";

            public const string Get_Partial_Databases = "Get_Partial_Databases";
            public const string Get_Partial_Databases_Response = "Get_Partial_Databases_Response";

            public const string Get_Id = "Get_Id";
            public const string Get_Id_Response = "Get_Id_Response";

            public const string Add_Database = "Add_Database";
            public const string Add_Database_Response = "Add_Database_Response";

            public const string Remove_Datababase = "Remove_Database";
            public const string Remove_Database_Response = "Remove_Database_Response";

            public const string Get_Pending_Process_Contracts = "Get_Pending_Process_Contracts";
            public const string Get_Pending_Process_Contracts_Respoonse = "Get_Pending_Process_Contracts_Response";

            public const string Accept_Pending_Contract = "Accept_Pending_Contract";
            public const string Accept_Pending_Contract_Response = "Accept_Pending_Contract_Response";

            public const string Reject_Pending_Contract = "Reject_Pending_Contract";
            public const string Reject_Pending_Contract_Response = "Reject_Pending_Contract_Response";
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

            public const string Get_Contract_Information = "Get_Contract_Info";
            public const string Get_Contract_Information_Response = "Get_Contract_Info_Response";

            public const string Get_Pending_Contracts = "Get_Pending_Contracts";
            public const string Get_Pending_Contracts_Response = "Get_Pending_Contracts_Response";

            public const string Get_Accepted_Contracts = "Get_Accepted_Contracts";
            public const string Get_Accepted_Contracts_Response = "Get_Accepted_Contracts_Response";

            public const string Update_Contract_Information = "Update_Contract_Info";
            public const string Update_Contract_Information_Response = "Update_Contract_Info_Response";

            public const string Add_Participant = "Add_Participant";
            public const string Add_Participant_Response = "Add_Participant_Response";

            public const string Remove_Participant = "Remove_Participant";
            public const string Remove_Participant_Response = "Remove_Participant_Response";
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
