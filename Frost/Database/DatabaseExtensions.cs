using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace FrostDB
{
    static class DatabaseExtensions
    {
        public static byte[] ToByteArray(this RowInsert row, Process process)
        {
            
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
