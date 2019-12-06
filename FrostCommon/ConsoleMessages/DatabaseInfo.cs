using System;
using System.Collections.Generic;
using System.Text;

namespace FrostCommon.ConsoleMessages
{
    public class DatabaseInfo
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public List<string> Tables { get; set; }

    }
}
