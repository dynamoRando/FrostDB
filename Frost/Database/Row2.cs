using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class Row2
    {
        /*
         * Row Byte Array Layout:
         * RowId IsLocal {{SizeOfRow | ParticipantId} | RowData}
         * RowId IsLocal - preamble (used in inital load of the Row)
         * 
         * if IsLocal == true, then need to request the rest of the byte array
         * 
         * if IsLocal == false, then need to request the rest of the byte array, i.e. the size of the ParticipantId
         * 
         * SizeOfRow is the size of the rest of the row in bytes minus the preamble. 
         * For a remote row, this is just the size of the ParticipantId (a guid)
         * For a local row, this is the total size of all the data
         * 
         * If IsLocal == true, format is as follows -
         * [data_col1] [data_col2] [data_colX] - fixed size columns first
         * [SizeOfVar] [varData] [SizeOfVar] [varData] - variable size columns
         */

        #region Private Fields
        private RowPreamble _preamble;
        private byte[] _data;
        private Guid _participantId;
        private List<ColumnSchema> _columns;
        private int _rowSize;
        private PageAddress _pageAddress;
        #endregion

        #region Public Properties
        public int RowId => _preamble.RowId;
        public int SizeOfParticipantId => DatabaseConstants.PARTICIPANT_ID_SIZE;
        public int SizeOfRowSize => DatabaseConstants.SIZE_OF_ROW_SIZE;
        public int RowSize => GetRowSize();
        public bool IsLocal => _preamble.IsLocal;
        public Guid ParticipantId => _participantId;
        public PageAddress PageAddress => _pageAddress;
        public byte[] BinaryData => _data;
        public RowPreamble Preamble => _preamble;
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        /// <summary>
        /// Creates an empty Row2 object with no values set
        /// </summary>
        public Row2() { }

        /// <summary>
        /// Constructs a Row2 object based on the binary preamble and the specified column schema. The column schema will be used to 
        /// parse the row binary data.
        /// </summary>
        /// <param name="preamble">The binrary preamble</param>
        /// <param name="columns">The column schema of the row</param>
        public Row2(byte[] preamble, List<ColumnSchema> columns)
        {
            _preamble = new RowPreamble(preamble);
            _columns = columns;
        }

        /// <summary>
        /// Constructs a Row2 object along with setting it's page address. 
        /// </summary>
        /// <param name="pageAddress">The page address of the row</param>
        /// <param name="preamble">The binary preamble of the row</param>
        /// <param name="columns">The column schema of the row</param>
        public Row2(PageAddress pageAddress, byte[] preamble, List<ColumnSchema> columns) : this(preamble, columns)
        {
            _pageAddress = pageAddress;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Sets the row data from a byte array. This data does not include the row preamble.
        /// </summary>
        /// <param name="data">The byte array representing the row data.</param>
        public void SetRowData(byte[] data)
        {
            _data = data;
            SetSizeOfRow();
        }

        public void SetRowData(RowInsert row)
        {
            row.Values.ToBinaryFormat();
        }

        #endregion

        #region Private Methods
        private int GetRowSize()
        {
            if (IsLocal)
            {
                return _rowSize;
            }
            else
            {
                return SizeOfParticipantId;
            }
        }
      
        private void ParseLocalRow()
        {
           // parse data based on column schema
        }

        private void ParseRemoteRow()
        {
            GetParticipantId();
        }

        private int GetRowIdOffset()
        {
            return 0;
        }

        private int GetSizeOfRowOffSet()
        {
            return 0;
        }

        private int GetSizeOfRow()
        {
            if (IsLocal)
            {
                var span = new ReadOnlySpan<Byte>(_data);
                var bytes = span.Slice(GetSizeOfRowOffSet(), SizeOfRowSize);
                return BitConverter.ToInt32(bytes);
            }
            else
            {
                return DatabaseConstants.PARTICIPANT_ID_SIZE;
            }
        }

        private int GetParticipantOffset()
        {
            return 0;
        }

        private Guid GetParticipantId() 
        {
            var span = new ReadOnlySpan<byte>(_data);
            var bytes = span.Slice(GetParticipantOffset(), SizeOfParticipantId);
            return new Guid(bytes);
        }

        private void SetParticipant()
        {
            var data = ParticipantId.ToByteArray();
            data.CopyTo(_data, GetParticipantOffset());
        }

        private void SetSizeOfRow()
        {
            if (IsLocal)
            {
                _rowSize = ComputeRowSize();
                var data = BitConverter.GetBytes(RowSize);
                data.CopyTo(_data, GetSizeOfRowOffSet());
            }
            else
            {
                // we do not need to save the RowSize value bc it is the same as the length of a participant id (a guid)
            }
        }

        private int ComputeRowSize()
        {
            _rowSize = 0;
            // need to compute the size of the row, starting with fixed size columns, then variable columns
            throw new NotImplementedException();
        }
        #endregion
    }

    public class RowPreamble
    {
        #region Private Fields
        private byte[] _data;
        #endregion

        #region Public Properties
        public int RowId => GetRowId();
        public bool IsLocal => GetIsLocal();
        public int SizeOfIsLocal => DatabaseConstants.SIZE_OF_IS_LOCAL;
        public int SizeOfRowId => DatabaseConstants.SIZE_OF_ROW_ID;
        public byte[] BinaryData => _data;
        public int Length => _data.Length;
        #endregion

        #region Constructors
        public RowPreamble(byte[] data)
        {
            _data = data;
            ParsePreamble();
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        private int GetRowId()
        {
            return BitConverter.ToInt32(_data, 0);
        }

        private bool GetIsLocal()
        {
            return BitConverter.ToBoolean(new ReadOnlySpan<byte>(_data).Slice(GetIsLocalOffset(), SizeOfIsLocal));
        }

        private void ParsePreamble()
        {
            GetRowId();
            GetIsLocal();
        }

        private int GetIsLocalOffset()
        {
            return SizeOfRowId;
        }
        #endregion

    }
}
