using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    class RowUpdate
    {
        public TableSchema2 Table { get; set; }
        public int RowId { get; set; }
        public List<RowValue2> Values { get; set; }

        public RowUpdate()
        {
            Values = new List<RowValue2>();
        }
    }
}
