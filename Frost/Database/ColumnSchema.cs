using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    /// <summary>
    /// Represents various properties of a column in a table
    /// </summary>
    public class ColumnSchema
    {
        #region Private Fields
        #endregion

        #region Public Properties
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
        /// The max size of the column in bytes. This is fixed for fixed size columns, but variable for VAR types
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// The order in which this column appears in the byte array. This != ordinal, which is how the table perceives the columns to be ordered.
        /// </summary>
        public int Order { get; set; }
        public bool IsVariableLength => isVariableLengthColumn();
        #endregion

        #region Constructors
        public ColumnSchema() { }

        public ColumnSchema(string name, string dataType)
        {
            Name = name;
            DataType = dataType;
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        private bool isVariableLengthColumn()
        {
            if (DataType.StartsWith("VARCHAR") || 
                DataType.StartsWith("NVARCHAR") ||
                DataType.StartsWith("NUMERIC") || 
                DataType.StartsWith("DECIMAL"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion


    }
}
