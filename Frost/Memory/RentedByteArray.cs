using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public struct RentedByteArray
    {
        public byte[] Array { get; set; }
        public int RequestedSize { get; set; }
        public int Length => RequestedSize;
    }
}
