using System;
using System.Collections.Generic;
using System.Text;

namespace FrostCommon.ConsoleMessages
{
    public class TableInfo
    {
        public Guid? TableId { get; set; }
        public Guid? DatabaseId { get; set; }
        public string TableName { get; set; }
        public List<string> Columns { get; set; }
    }
}
