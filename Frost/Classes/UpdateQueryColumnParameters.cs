using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Classes
{
    public class UpdateQueryColumnParameters
    {
        public string ColumnName { get; set; }
        public Guid? ColumnId { get; set; }
        public Guid? TableId { get; set; }
        public string TableName { get; set; }
        public object Value { get; set; }
        public Type  ColumnType { get; set; }
    }
}
