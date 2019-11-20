using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IContract 
    {
        string DatabaseName { get; set; }
        Guid? DatabaseId { get; set; }
        Location DatabaseLocation { get; set; }
        DbSchema DatabaseSchema { get; set; }
        string ContractDescription { get; set; }
        Guid? ContractId { get; set; }
        Guid? ContractVersion { get; set; }
        Guid? ProcessId { get; set; }
        bool IsAccepted { get; set; }
        DateTime AcceptedDateTime { get; set; }
        DateTime SentDateTime { get; set; }
    }
}
