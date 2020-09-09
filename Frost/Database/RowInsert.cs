using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    /// <summary>
    /// Holds information about a row to be inserted
    /// </summary>
    public class RowInsert
    {
        public TableSchema2 Table { get; set; }
        public List<RowValue2> Values { get; set; }

        public RowInsert()
        {
            Values = new List<RowValue2>();
        }
    }
}
