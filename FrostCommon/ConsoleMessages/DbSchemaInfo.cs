using System;
using System.Collections.Generic;
using System.Text;

namespace FrostCommon.ConsoleMessages
{
    public class DbSchemaInfo
    {
        public string DatabaseName { get; set; }
        public Guid? DatabaseId { get; set; }
        public List<TableSchemaInfo> Tables { get; set; }

        public DbSchemaInfo()
        {
            Tables = new List<TableSchemaInfo>();
        }
    }
}
