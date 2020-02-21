using System;
using System.Collections.Generic;
using System.Text;

namespace FrostCommon.ConsoleMessages
{
    public class AcceptedContractInfo
    {
        public List<string> AcceptedContracts { get; set; }
        public string DatabaseName { get; set; }
        public Guid? DatabaseId { get; set; }

        public AcceptedContractInfo()
        {
            this.AcceptedContracts = new List<string>();
        }
    }
}
