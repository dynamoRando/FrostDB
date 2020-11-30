using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace FrostDB
{
    public class RowValue2
    {
        #region Private Fields
        #endregion

        #region Public Properties
        public ColumnSchema Column { get; set; }
        public string Value { get; set; }
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        /// <summary>
        /// Returns the value in binary array format. If the value is of variable length, includes a 4 byte integer
        /// sizeOf prefix
        /// </summary>
        /// <returns>The value in binary array format (includes if applicable a sizeOf prefix)</returns>
        public byte[] GetValueBinaryArrayWithSizePrefix()
        {
            byte[] data = null;
            data = GetValueBinaryArray();
            if (Column.IsVariableLength)
            {
                AddLengthPrefix(ref data);
            }

            return data;
        }

        /// <summary>
        /// Returns the value in binary array format (does not include a sizeOf prefix)
        /// </summary>
        /// <returns>The value in binary array format (does not include sizeOf prefix)</returns>
        public byte[] GetValueBinaryArray()
        {
            byte[] data = null;

            Debug.WriteLine(this.Value);

            if (Column.DataType.Contains("CHAR"))
            {
                data = DatabaseBinaryConverter.StringToBinary(Value, Column.DataType);
            }

            if (Column.DataType.Contains("DECIMAL") || Column.DataType.Contains("NUMERIC"))
            {
                data = DatabaseBinaryConverter.DecimalToBinary(Value, Column.DataType);
            }

            if (Column.DataType.Contains("DATETIME"))
            {
                data = DatabaseBinaryConverter.DateTimeToBinary(Value);
            }

            if (Column.DataType.Contains("BIT"))
            {
                data = DatabaseBinaryConverter.BooleanToBinary(Value);
            }

            if (Column.DataType.Equals("INT"))
            {
                data = BitConverter.GetBytes(Convert.ToInt32(Value));
            }

            return data;
        }

        /// <summary>
        /// Gets the binary length of the value. If the column is of variable length, will include the 4 byte integer size
        /// to store the sizeOf prefix
        /// </summary>
        /// <returns>The binary length of the value (including the sizeOf, if appropriate)</returns>
        public int GetValueBinaryLength()
        {
            int totalSize = 0;
            byte[] data = GetValueBinaryArray();
          
            if (Column.IsVariableLength)
            {
                AddLengthPrefix(ref data);
            }

            totalSize = data.Length;

            return totalSize;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Adds a size prefix to the array (an int32)
        /// </summary>
        /// <param name="data">The array to get the size of</param>
        /// <returns>A new array with the size prefix added</returns>
        private static void AddLengthPrefix(ref byte[] data)
        {
            int dataLength = data.Length;
            byte[] originalData = ArrayPool<byte>.Shared.Rent(dataLength);
            data.CopyTo(originalData, 0);

            byte[] dataLengthBytes = BitConverter.GetBytes(dataLength);
            Array.Resize<byte>(ref data, dataLength + DatabaseConstants.SIZE_OF_INT);
            Array.Copy(dataLengthBytes, 0, data, 0, dataLengthBytes.Length);
            Array.Copy(originalData, 0, data, DatabaseConstants.SIZE_OF_INT, dataLength);

            Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name} dataLength: {dataLength.ToString()}");

            ArrayPool<byte>.Shared.Return(originalData, true);
        }
        #endregion


    }
}
