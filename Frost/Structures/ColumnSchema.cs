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
        public int Size => GetSizeForDataType();
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
        /// <summary>
        /// Parses a byte array to a RowValue2 for this column data type. This method assumes the byte array passed in is the value. If the column length is variable, you must ensure the array size passed in
        /// is the array for the total value (do not include the size prefix for variable length columns.)
        /// </summary>
        /// <param name="span">The byte array to parse. This array must contain the value to be parsed. Ensure that it does not contain the size prefix for variable length columns.</param>
        /// <returns>A RowValue2</returns>
        public RowValue2 Parse(ReadOnlySpan<byte> span)
        {
            var value = new RowValue2();
            value.Column = this;

            if (!isVariableLengthColumn())
            {
                if (DataType.Equals("DATETIME"))
                {
                    value.Value = DatabaseBinaryConverter.BinaryToDateTime(span).ToString();
                }

                if (DataType.Equals("BIT"))
                {
                    value.Value = DatabaseBinaryConverter.BinaryToBoolean(span).ToString();
                }

                if (DataType.Equals("INT"))
                {
                    value.Value = DatabaseBinaryConverter.BinaryToInt(span).ToString();
                }

                if (DataType.Contains("CHAR"))
                {
                    // need to get the fixed character width, then parse
                    // CHAR always pads any values less with whitespace
                    throw new NotImplementedException();
                }
            }
            else
            {
                // if it's varaible length, then the array has prefix'ed the actual value with the size (length) of the item. The prefix is the size of an INT32.
                // example VARCHAR(10)
                // byte format is 2 <byte array>
                // in the above if the value was "XX" it means that we only used 2 characters out of a potential maximum of 10 characters wide

                if (DataType.Contains("VARCHAR"))
                {
                    value.Value = DatabaseBinaryConverter.BinaryToString(span).Trim();
                }

                if (DataType.Contains("DECIMAL"))
                {
                    throw new NotImplementedException();
                }

                if (DataType.Contains("NUMERIC"))
                {
                    throw new NotImplementedException();
                }
            }

            return value;
        }

        /// <summary>
        /// Parses a byte array to a RowValue2 for this column data type. This method assumes the byte array passed in is the value. If the column length is variable, you must ensure the array size passed in
        /// is the array for the total value (do not include the size prefix for variable length columns.)
        /// </summary>
        /// <param name="span">The byte array to parse. This array must contain the value to be parsed. Ensure that it does not contain the size prefix for variable length columns.</param>
        /// <returns>A RowValue2</returns>
        public RowValue2 Parse(Span<byte> span)
        {
            var value = new RowValue2();
            value.Column = this;

            if (!isVariableLengthColumn())
            {
                if (DataType.Equals("DATETIME"))
                {   
                    value.Value = DatabaseBinaryConverter.BinaryToDateTime(span).ToString();
                }

                if (DataType.Equals("BIT"))
                {
                    value.Value = DatabaseBinaryConverter.BinaryToBoolean(span).ToString();
                }

                if (DataType.Equals("INT"))
                {
                    value.Value = DatabaseBinaryConverter.BinaryToInt(span).ToString();
                }

                if (DataType.Contains("CHAR"))
                {
                    // need to get the fixed character width, then parse
                    // CHAR always pads any values less with whitespace
                    throw new NotImplementedException();
                }
            }
            else
            {
                // if it's varaible length, then the array has prefix'ed the actual value with the size (length) of the item. The prefix is the size of an INT32.
                // example VARCHAR(10)
                // byte format is 2 <byte array>
                // in the above if the value was "XX" it means that we only used 2 characters out of a potential maximum of 10 characters wide

                if (DataType.Contains("VARCHAR"))
                {
                    throw new NotImplementedException();
                }

                if (DataType.Contains("DECIMAL"))
                {
                    throw new NotImplementedException();
                }

                if (DataType.Contains("NUMERIC"))
                {
                    throw new NotImplementedException();
                }
            }

            return value;
        }

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

        /// <summary>
        /// Returns the byte length for this column's data type
        /// </summary>
        /// <returns>The length of bytes for this column</returns>
        private int GetSizeForDataType()
        {
            int size = 0;

            if (DataType.Equals("BIT"))
            {
                size = DatabaseConstants.SIZE_OF_BOOL;
            }

            if (DataType.Equals("DATETIME"))
            {
                size = DatabaseConstants.SIZE_OF_DATETIME;
            }

            if (DataType.Equals("INT"))
            {
                size = DatabaseConstants.SIZE_OF_INT;
            }

            if (DataType.Contains("CHAR"))
            {
                // need to get the fixed character width, then parse
                throw new NotImplementedException();
            }

            if (DataType.Contains("DECIMAL"))
            {
                throw new NotImplementedException();
            }

            if (DataType.Contains("NUMERIC"))
            {
                throw new NotImplementedException();
            }

            return size;
        }

        #endregion


    }
}
