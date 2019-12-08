using System;
using System.Collections.Generic;
using System.Text;

namespace FrostCommon.ConsoleMessages
{
    public class DatabaseInfo
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// A list of tables in the database. The TableId, and the Name of the table
        /// </summary>
        public List<(Guid?, string)> Tables { get; }

        public DatabaseInfo()
        {
            Tables = new List<(Guid?, string)>();
        }

        public void AddToTables((Guid?, string) value)
        {
            Tables.Add(value);
        }
    }
}
