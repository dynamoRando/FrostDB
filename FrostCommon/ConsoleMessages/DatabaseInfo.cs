using System;
using System.Collections.Generic;
using System.Text;

namespace FrostCommon.ConsoleMessages
{
    public class DatabaseInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// A list of tables in the database. The TableId, and the Name of the table
        /// </summary>
        public List<(string, string)> Tables { get; }

        public DatabaseInfo()
        {
            Tables = new List<(string, string)>();
        }

        public void AddToTables((string?, string) value)
        {
            Tables.Add(value);
        }
    }
}
