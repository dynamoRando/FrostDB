using System;
using System.Collections.Generic;
using System.Text;

namespace FrostCommon.ConsoleMessages
{
    public class PendingContractInfo
    {
        public List<string> PendingContracts { get; set; }
        public string DatabaseName { get; set; }
        public Guid? DatabaseId { get; set; }

        public PendingContractInfo()
        {
            this.PendingContracts = new List<string>();
        }
    }
}
