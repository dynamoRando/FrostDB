using System;
using System.Collections.Generic;
using System.Text;

namespace FrostCommon.ConsoleMessages
{
    public class TableInfo
    {
        public Guid? TableId { get; set; }
        public Guid? DatabaseId { get; set; }
        public string TableName { get; set; }
        public string DatabaseName { get; set; }
        /// <summary>
        /// A list of column information: string is the name of the column, type is the column type (int, string, etc.)
        /// </summary>
        public List<(string, string)> Columns { get; set; }

        public TableInfo()
        {
            Columns = new List<(string, string)>();
        }
    }
}
