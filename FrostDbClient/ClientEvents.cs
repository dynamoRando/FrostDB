using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDbClient
{
    public static class ClientEvents
    {
        public const string GotProcessId = "Process.Got_Id";
        public const string GotDatabaseNames = "Process.Got_Database_Names";
        public const string GotPartialDatabaseNames = "Process.Got_Partial_Database_Names";
        public const string GotDatabaseInfo = "Database.Got_Information";
        public const string GotTableInfo = "Table.Got_Information";
        public const string GotDatabaseContractInfo = "Database.Got_Contract_Information";
        public const string GotPendingContractInfo = "Database.Got_Pending_Contract_Information";
        public const string GetProcessPendingContractInfo = "Process.Got_Process_Pending_Contract_Information";
        public const string GotAcceptedContractsInfo = "Database.Got_Accepted_Contract_Info";
    }
}
