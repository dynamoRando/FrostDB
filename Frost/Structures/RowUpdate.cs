using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    /// <summary>
    /// Holds information for rows to be updated in a table
    /// </summary>
    public class RowUpdate
    {
        private Guid _xactId;

        public TableSchema2 Table { get; set; }
        public int RowId { get; set; }
        public List<RowValue2> Values { get; set; }
        public Guid XactId => _xactId;

        public RowUpdate()
        {
            Values = new List<RowValue2>();
            _xactId = Guid.NewGuid();
        }
    }
}
