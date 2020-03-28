using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class MessageDataAction
    {
        public class Contract
        {
            public const string Save_Pending_Contract = "Contract.Save_Pending";
            public const string Save_Pending_Contract_Recieved = "Contract.Save_Pending_Recieved";
            public const string Accept_Pending_Contract = "Contract.Accept_Pending";
            public const string Accept_Pending_Contract_Recieved = "Contract.Accept_Pending_Recieved";
            public const string Decline_Pending_Contract = "Contract.Decline_Pending";
            public const string Decline_Pending_Contract_Recieved = "Contract.Decline_Pending_Recieved";
        }

        public class Row
        {
            public const string Save_Row = "Row.Save";
            public const string Save_Row_Response = "Row.Save_Reponse";
            public const string Update_Row = "Row.Update";
            public const string Update_Row_Response = "Row.Update_Response";
        }

        public class Status
        {
            public const string Is_Online = "Status.Is_Online";
            public const string Is_Online_Response = "Status.Is_Online_Status";
        }

    }
}
