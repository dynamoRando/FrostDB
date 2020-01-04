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
        /// <summary>
        /// TableName, Permission Owner, Permissions
        /// </summary>
        public List<(string, List<(string, List<string>)>)> SchemaData { get; set; }


        public ContractInfo()
        {
            TableNames = new List<string>();
            SchemaData = new List<(string, List<(string, List<string>)>)>();
        }
    }
}
