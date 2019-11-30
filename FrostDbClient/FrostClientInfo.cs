using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDbClient
{
    public class FrostClientInfo
    {
        public Guid? ProcessId { get; set; }
        public List<string> DatabaseNames { get; set; }
    }
}
