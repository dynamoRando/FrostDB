using System;
using System.Collections.Generic;
using System.Text;

namespace FrostCommon.ConsoleMessages
{
    public class DatabaseInfo
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public List<(Guid?, string)> Tables { get; set; }

        public DatabaseInfo()
        {
            Tables = new List<(Guid?, string)>();
        }
    }
}
