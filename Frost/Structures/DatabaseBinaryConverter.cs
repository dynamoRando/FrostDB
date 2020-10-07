using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Collectathon.DataStructures;
using Collectathon.DataStructures.Arrays;

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
            byte[] result = null;
            // to do: need to parse the column definition to make sure that the size of the string field is not 
            // longer than the actual value

            // VARCHAR(20)
            value = value.Replace("'", string.Empty);

            string[] lengthDefinition = columnDefinition.Split('(', ')');
            int length = Convert.ToInt32(lengthDefinition[1]);
            int indexLengthStart = columnDefinition.IndexOf('(');
            string definition = columnDefinition.Substring(0, indexLengthStart);

            if (definition.Equals("VARCHAR"))
            {
                var item = new BoundedArray<char>(length);
                var items = value.ToCharArray();
                foreach(var x in items)
                {
                    item.Add(x);
                }

                if (item.Length < length)
                {
                    int spacesRemaining =  length - item.Length;
                    for(int x = item.Length + 1; x < length; x++ )
                    {
                        item[x] = Char.MinValue; // will I regret this? probably. 
                    }
                }

                char[] temp = new char[length];
                int k = 0;
                foreach(var y in item)
                {
                    temp[k] = y;
                    k++;
                }

                result = Encoding.UTF8.GetBytes(temp);

            }

            if (definition.Equals("NVARCHAR"))
            {
                throw new NotImplementedException();
            }

            if (definition.Equals("CHAR"))
            {
                throw new NotImplementedException();
            }


            // this method should handle the following SQL types: NVARCHAR, VARCHAR, CHAR
            return result;
        }

        /// <summary>
        /// Converts a string value to a binary array (UTF8)
        /// </summary>
        /// <param name="value">The string value to convert to bytes</param>
        /// <returns>A byte array representation of the string</returns>
        public static byte[] StringToBinary(string value)
        {
            return Encoding.UTF8.GetBytes(value);
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
            if (value.ToUpper() == "TRUE" || value.Equals("1"))
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
        /// Converts a binary array to int
        /// </summary>
        /// <param name="span">The binary array</param>
        /// <returns>The int</returns>
        public static int BinaryToInt(ReadOnlySpan<byte> span)
        {
            return BitConverter.ToInt32(span);
        }

        /// <summary>
        /// Converts bytes to a boolean value
        /// </summary>
        /// <param name="span">The bytes</param>
        /// <returns>The boolean value</returns>
        public static bool BinaryToBoolean(ReadOnlySpan<byte> span)
        {
            return BitConverter.ToBoolean(span);
        }

        /// <summary>
        /// Converts an array of bytes to a DateTime value
        /// </summary>
        /// <param name="bytes">The bytes to be parsed</param>
        /// <returns>A DateTime object</returns>
        public static DateTime BinaryToDateTime(ReadOnlySpan<byte> bytes)
        {
            long item = BitConverter.ToInt64(bytes);
            return DateTime.FromBinary(item);
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

        /// <summary>
        /// Converts a Guid to binary array
        /// </summary>
        /// <param name="guid">The guid to convert</param>
        /// <returns>The binary representation of the GUID</returns>
        public static byte[] GuidToBinary(Guid guid)
        {
            return guid.ToByteArray();
        }

        /// <summary>
        /// Converts a byte array to a Guid
        /// </summary>
        /// <param name="span">The Guid binary array</param>
        /// <returns>A Guid</returns>
        public static Guid BinaryToGuid(Span<byte> span)
        {
            return new Guid(span);
        }

        /// <summary>
        /// Returns a string based on the supplied binary array (assumes UTF8)
        /// </summary>
        /// <param name="bytes">The binary array</param>
        /// <returns>The string represented by the byte array</returns>
        public static string BinaryToString(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
