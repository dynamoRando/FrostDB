using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    /// <summary>
    /// Sorts RowValues based on the Ordinal, then Variable fields. This is to order the row to binary format.
    /// </summary>
    class RowValueSorter : IComparer<RowValue2>
    {
        public int Compare(RowValue2 x, RowValue2 y)
        {
            // need to return 1 for greater than, -1 for less than, or 0 if equal

            // need to put the fixed length columns first, then sort by ordinal

            int result = x.Column.IsVariableLength.CompareTo(y.Column.IsVariableLength);
            if (result == 0)
            {
                if (x.Column.Ordinal > y.Column.Ordinal)
                {
                    return 1;
                }

                if (x.Column.Order < y.Column.Ordinal)
                {
                    return -1;
                }

                else
                {
                    return 0;
                }
            }
            return result;
        }

    }
}
