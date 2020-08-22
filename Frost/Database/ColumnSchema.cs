using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class ColumnSchema
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public int Index { get; set; }
        public bool IsNullable { get; set; }
    }
}
