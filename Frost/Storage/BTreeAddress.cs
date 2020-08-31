using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    /// <summary>
    /// Used to identify which table a B-Tree belongs to
    /// </summary>
    class BTreeAddress
    {
        public int DatabaseId { get; set;}
        public int TableId { get; set; }
    }
}
