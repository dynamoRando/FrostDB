using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class MessageAction
    {
        public class Contract
        {
            public const string Save_Pending_Contract = "Save_Pending_Contract";
            public const string Save_Pending_Contract_Recieved = "Save_Pending_Contract_Recieved";
            public const string Accept_Pending_Contract = "Accept_Pending_Contract";
            public const string Accept_Pending_Contract_Recieved = "Accept_Pending_Contract_Recieved";
            public const string Decline_Pending_Contract = "Decline_Pending_Contract";
            public const string Decline_Pending_Contract_Recieved = "Decline_Pending_Contract_Recieved";
        }

        public class Row
        {
            public const string Save_Row = "Save_Row";
            public const string Update_Row = "Update_Row";
        }

    }
}
