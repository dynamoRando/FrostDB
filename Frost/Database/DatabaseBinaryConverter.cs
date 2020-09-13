using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    /// <summary>
    /// Converts object values to a binary array based on the column definition if applicable
    /// </summary>
    static class DatabaseBinaryConverter
    {
        /// <summary>
        /// Converts a string value to a binary array based on the column definition
        /// </summary>
        /// <param name="value">The value to convert</param>
        /// <param name="columnDefinition">The column definition (Example: "VARCHAR(10)")</param>
        /// <returns>A byte array of the value</returns>
        /// <exception cref="System.InvalidOperationException">Thrown if the column definition size is greater than the actual value</exception>
        public static byte[] StringToBinary(string value, string columnDefinition)
        {
            // to do: need to parse the column definition to make sure that the size of the string field is not 
            // longer than the actual value

            // this method should handle the following SQL types: NVARCHAR, VARCHAR, CHAR
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts a decimal or numeric value to a binary array based on the column definition
        /// </summary>
        /// <param name="value">The value to convert</param>
        /// <param name="columnDefinition">The column definition (Example: "NUMERIC(10, 2)")</param>
        /// <returns>A binary array of the value</returns>
        /// <exception cref="System.InvalidOperationException">Thrown if the column definition size is greater than the actual value</exception>
        public static byte[] DecimalToBinary(string value, string columnDefinition)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts a datetime value to a binary array
        /// </summary>
        /// <param name="value">The value to convert</param>
        /// <returns>A binary array</returns>
        /// <exception cref="System.InvalidOperationException">Thrown if the data cannot be converted</exception>
        public static byte[] DateTimeToBinary(string value)
        {
            DateTime item;
            byte[] result = null;
            if (DateTime.TryParse(value, out item))
            {
                result = BitConverter.GetBytes(item.ToBinary());
            }
            else
            {
                throw new InvalidOperationException($"could not convert {value} to DATETIME");
            }

            return result;
        }

        /// <summary>
        /// Converts a boolean value to binary array
        /// </summary>
        /// <param name="value">The value to convert</param>
        /// <returns>A binary array</returns>
        public static byte[] BooleanToBinary(string value)
        {
            bool item;
            if (value == "TRUE" || value.Equals("1"))
            {
                item = true;
            }
            else
            {
                item = false;
            }

            return BitConverter.GetBytes(item);
        }

        /// <summary>
        /// Converts an array of bytes to a DateTime value
        /// </summary>
        /// <param name="bytes">The bytes to be parsed</param>
        /// <returns>A DateTime object</returns>
        public static DateTime BinaryToDateTime(byte[] bytes)
        {
            long item = BitConverter.ToInt64(bytes);
            return DateTime.FromBinary(item);
        }
    }
}
