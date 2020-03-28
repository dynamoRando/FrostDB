using System;
using System.Collections.Generic;
using System.Text;

namespace FrostCommon.ConsoleMessages
{
    public class TableSchemaInfo
    {
        public string TableName { get; set; }
        public List<ColumnSchemaInfo> Columns { get; set; }

        public TableSchemaInfo() 
        {
            Columns = new List<ColumnSchemaInfo>();
        }
    }
}
