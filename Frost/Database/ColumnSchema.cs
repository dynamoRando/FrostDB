using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class ColumnSchema
    {
        /// <summary>
        /// The name of the column
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The SQL data type of the column
        /// </summary>
        public string DataType { get; set; }
        /// <summary>
        /// The ordinal index of the column
        /// </summary>
        public int Ordinal { get; set; }
        /// <summary>
        /// Specifies if the column allows NULLs or not
        /// </summary>
        public bool IsNullable { get; set; }
        /// <summary>
        /// The size of the column in bytes
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// The byte offset of the column in relation <strong>to the order in the row</strong>
        /// </summary>
        public int Offset { get; set; }
    }
}
