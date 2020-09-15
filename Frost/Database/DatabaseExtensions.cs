using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace FrostDB
{
    static class DatabaseExtensions
    {
        #region Public Methods
        /// <summary>
        /// Computes the total binary size of the values
        /// </summary>
        /// <param name="values">A list of row values</param>
        /// <returns>The total binary size of the values</returns>
        public static int ComputeTotalSize(this List<RowValue2> values)
        {
            values.OrderByByteFormat();
            int totalSize = 0;

            foreach (var value in values)
            {
                totalSize += value.GetValueBinaryLength();
            }

            return totalSize;
        }

        /// <summary>
        /// Returns the data in binary format and order for the list of values. This will not include a row preamble.
        /// Includes the size of variable columns as a prefix in between each column.
        /// </summary>
        /// <returns>Row values in a binary array</returns>
        public static byte[] ToBinaryFormat(this List<RowValue2> values)
        {
            values.OrderByByteFormat();

            byte[] result;
            byte[] data = null;
            var list = new List<byte[]>();

            foreach (var value in values)
            {
                data = value.GetValueBinaryArrayWithSizePrefix();
                list.Add(data);
            }

            result = new byte[GetBinaryArraySize(list)];
            CopyItemsToArray(ref list, ref result);

            return result;
        }

        /// <summary>
        /// Orders the columns by non-variable columns first, then by ordinal number
        /// </summary>
        /// <param name="schema">The list of columns schema to be ordered</param>
        public static void OrderByByteFormat(this List<ColumnSchema> columns)
        {
            columns.OrderBy(column => column.IsVariableLength).ThenBy(column => column.Ordinal);
        }

        /// <summary>
        /// Sets the order number based on byte format
        /// </summary>
        /// <param name="columns">The list of columns to set the order number for</param>
        public static void SetOrderNumber(this List<ColumnSchema> columns)
        {
            columns.OrderByByteFormat();

            int i = 1;

            foreach (var column in columns)
            {
                column.Order = i;
                i++;
            }
        }

        /// <summary>
        /// Orders the values by non-variable columns first, then by ordinal number
        /// </summary>
        /// <param name="values">A list of row values to be sorted</param>
        public static void OrderByByteFormat(this List<RowValue2> values)
        {
            values.OrderBy(v => v.Column.IsVariableLength).ThenBy(v => v.Column.Ordinal);
        }

        /// <summary>
        /// Compares the Process Id against the RowForm's Participant Id. If they are the same, it means this row should be saved to this FrostDb Process.
        /// </summary>
        /// <param name="row">The rowForm to validate</param>
        /// <param name="process">The FrostDb Process</param>
        /// <returns>True if the row participant and the FrostDb process are the same, otherwise false</returns>
        public static bool IsLocal(this RowForm2 row, Process process)
        {

            if (row == null)
            {
                throw new ArgumentNullException(nameof(row));
            }

            if (row.Participant == null)
            {
                throw new InvalidOperationException("Participant is null");
            }

            return (row.Participant.Id == process.Id);
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Returns the total size of the arrays in the list
        /// </summary>
        /// <param name="items">A list of binary arrays</param>
        /// <returns>The total size of the arrays in the list</returns>
        private static int GetBinaryArraySize(List<byte[]> items)
        {
            int result = 0;

            foreach (var item in items)
            {
                result += item.Length;
            }

            return result;
        }

        /// <summary>
        /// Copies the items in the list of binary arrays to the specified array
        /// </summary>
        /// <param name="arrays">A list of binary arrays</param>
        /// <param name="destinationArray">The array to copy the items to</param>
        private static void CopyItemsToArray(ref List<byte[]> arrays, ref byte[] destinationArray)
        {
            int totalOffset = 0;

            foreach (var item in arrays)
            {
                totalOffset += item.Length;
                Array.Copy(item, 0, destinationArray, totalOffset, item.Length);
            }
        }

        #endregion
    }
}
