using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class InsertQueryParam
    {
        public string ColumnName { get; set; }
        public string TableName { get; set; }
        public string DatabaseName { get; set; }
        public Object Value { get; set; }
        public Column Column { get; set; }
        public int Index { get; set; }
    }
}
