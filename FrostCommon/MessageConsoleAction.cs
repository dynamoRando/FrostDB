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
        }

        public class Database
        {
            public const string Get_Database_Info = "Database.Get_Info";
            public const string Get_Database_Info_Response = "Database.Get_Info_Response";
        }
    }
}
