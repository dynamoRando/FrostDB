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
        private Guid _xactId;

        public TableSchema2 Table { get; set; }
        public List<RowValue2> Values { get; set; }
        public Guid XactId => _xactId;
        public Guid? ParticipantId { get; set; }

        public RowInsert()
        {
            Values = new List<RowValue2>();
            _xactId = Guid.NewGuid();
        }
    }
}
