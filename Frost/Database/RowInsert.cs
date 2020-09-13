using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using FrostDB.Extensions;

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
        /// Returns the data in binary format for this row. This does not include the row preamble.
        /// </summary>
        /// <returns>Row values in a binary array</returns>
        public byte[] ToBinaryFormat()
        {
            return _values.ToBinaryFormat();
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
        #endregion
    }
}
