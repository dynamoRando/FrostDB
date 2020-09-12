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

            foreach(var column in columns)
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
        /// Orders the values by non-variable columns first, then by the ordinal number
        /// </summary>
        /// <param name="row">The row insert parameter to be sorted</param>
        public static void OrderByByteFormat(this RowInsert row)
        {
            row.Values.OrderByByteFormat();
        }

        public static byte[] ToByteArray(this RowInsert row, Process process)
        {
            row.OrderByByteFormat();
            

            throw new NotImplementedException();
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
    }
}
