using System;
using System.Collections.Generic;
using System.Text;

namespace FrostCommon.DataMessages
{
    public class RemoteRowInfo
    {
        public Guid? DatabaseId { get; set; }
        public string DatabaseName { get; set; }
        public Guid? TableId { get; set; }
        public string TableName { get; set; }
        public Guid? RowId { get; set; }
    }
}
