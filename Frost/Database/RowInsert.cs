using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using FrostDB.Extensions;
using System.Buffers;
using Newtonsoft.Json.Converters;

namespace FrostDB
{
    /// <summary>
    /// Holds information about a row to be inserted
    /// </summary>
    public class RowInsert
    {
        #region Private Fields
        private Guid _xactId;
        private List<RowValue2> _values;
        private TableSchema2 _table;
        private Guid? _participantId;
        private bool _isReferenceInsert;
        #endregion

        #region Public Properties
        /// <summary>
        /// The table that this row belongs to
        /// </summary>
        public TableSchema2 Table => _table;

        /// <summary>
        /// The values to be inserted
        /// </summary>
        public List<RowValue2> Values => _values;

        /// <summary>
        /// The xaction id of this operation
        /// </summary>
        public Guid XactId => _xactId;

        /// <summary>
        /// The participant id for this insert (who is this insert for?)
        /// </summary>
        public Guid? ParticipantId => _participantId;

        /// <summary>
        /// Returns the binary size of the row
        /// </summary>
        public int Size => ComputeTotalSize();

        /// <summary>
        /// Indicates if the insert is referential only. This value is true if the row should save only the participant id 
        /// (indicating a remote insert) and false if the the row should be saved to this FrostDb process. When this action
        /// occurs on a participants FrostDb process, this should be preserved.
        /// </summary>
        public bool IsReferenceInsert => _isReferenceInsert;

        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public RowInsert(List<RowValue2> values, TableSchema2 table, Guid? participantId, bool isReferenceInsert)
        {
            _values = values;
            _table = table;
            _participantId = participantId;
            _xactId = Guid.NewGuid();
            SortByBinaryFormat();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Orders the values by non-variable columns first, then by the ordinal number
        /// </summary>
        /// <param name="row">The row insert parameter to be sorted</param>
        public void OrderByByteFormat()
        {
            _values.OrderByByteFormat();
        }

        /// <summary>
        /// Returns the data in binary format for this row. This does not include the row preamble.
        /// </summary>
        /// <returns>Row values in a binary array</returns>
        public byte[] ToBinaryFormat()
        {
            return _values.ToBinaryFormat();
        }

        /// <summary>
        /// Returns the data in binary format for this row. This method takes an array reference (usually passed in from ArrayPool). 
        /// This does not include the row preamble. If local, it will prefix with the size of the row.
        /// </summary>
        /// <param name="returnedArray">A source array reference (usually from ArrayPool)</param>
        /// <returns>Row values in a binary array</returns>
        public void ToBinaryFormat(ref byte[] array)
        {
            if (!IsReferenceInsert)
            {
                Values.OrderByByteFormat();
                int currentOffset = 0;
                
                // need to save off the total row size first before adding the row data
                byte[] rowSizeArray = BitConverter.GetBytes(Size);
                Array.Copy(rowSizeArray, 0, array, currentOffset, rowSizeArray.Length);
                currentOffset += rowSizeArray.Length;

                // add the row data
                foreach (var value in Values)
                {
                    byte[] item = value.GetValueBinaryArrayWithSizePrefix();
                    Array.Copy(item, 0, array, currentOffset, item.Length);
                    currentOffset += item.Length;
                }
            }
            else
            {
                // just save off the participant id (the GUID)
                byte[] item = DatabaseBinaryConverter.GuidToBinary(ParticipantId.Value);
                Array.Copy(item, 0, array, 0, item.Length);
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Sorts the values in this RowInsert object by binary order
        /// </summary>
        private void SortByBinaryFormat()
        {
            _values.OrderByByteFormat();
        }

        private int ComputeTotalSize()
        {
            Values.OrderByByteFormat();
            int totalSize = 0;

            foreach (var value in Values)
            {
                totalSize += value.GetValueBinaryLength();
            }

            return totalSize;
        }
        #endregion
    }
}
