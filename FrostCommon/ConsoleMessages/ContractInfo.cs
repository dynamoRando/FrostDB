using System;
using System.Collections.Generic;
using System.Text;

namespace FrostCommon.ConsoleMessages
{
    public class ContractInfo
    {
        public string DatabaseName { get; set; }
        public Guid? ContractId { get; set; }
        public Guid? ContractVersion { get; set; }
        public List<string> TableNames { get; set; }
        
    }
}
