using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public struct RowStruct
    {
        public int RowId { get; set; }
        public bool IsLocal { get; set; }
        public int RowSize { get; set; }
        public Guid ParticipantId { get; set; }
        public RowValue2[] Values { get; set; }
    }
}
