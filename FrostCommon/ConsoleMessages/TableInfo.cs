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
        public string DatabaseName { get; set; }
        public List<(string, Type)> Columns { get; set; }

        public TableInfo()
        {
            Columns = new List<(string, Type)>();
        }
    }
}
