﻿using System;
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
        private byte[] _preamble;
        private byte[] _data;
        private bool _isLocal;
        private Guid _participantId;
        private List<ColumnSchema> _columns;
        private int _rowId;
        private int _rowSize;
        #endregion

        #region Public Properties
        public int RowId => _rowId;
        public int SizeOfParticipantId => DatabaseConstants.PARTICIPANT_ID_SIZE;
        public int SizeOfIsLocal => DatabaseConstants.SIZE_OF_IS_LOCAL;
        public int SizeOfRowId => DatabaseConstants.SIZE_OF_ROW_ID;
        public int SizeOfRowSize => DatabaseConstants.SIZE_OF_ROW_SIZE;
        public int RowSize => GetRowSize();
        public bool IsLocal => _isLocal;
        public Guid ParticipantId => _participantId;
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Row2() { }
        public Row2(byte[] preamble, List<ColumnSchema> columns)
        {
            _preamble = preamble;
            _columns = columns;

            ParsePreamble();
        }
        #endregion

        #region Public Methods
        public void SetRowData(byte[] data)
        {
            _data = data;
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
        private void ParsePreamble()
        {
            GetRowId();
            GetIsLocal();
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

        private int GetIsLocalOffset()
        {
            return SizeOfRowId;
        }

        private int GetSizeOfRow()
        {
            if (IsLocal)
            {
                var span = new Span<Byte>(_data);
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

        private int GetRowId()
        {
            var span = new Span<byte>(_preamble);
            var bytes = span.Slice(0, SizeOfRowId);
            return BitConverter.ToInt32(bytes);
        }

        private void SetRowId()
        {
            var data = BitConverter.GetBytes(RowId);
            data.CopyTo(_preamble, GetRowIdOffset());
        }

        private Guid GetParticipantId() 
        {
            var span = new Span<byte>(_data);
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

        private void SetIsLocal()
        {
            var data = BitConverter.GetBytes(IsLocal);
            data.CopyTo(_preamble, GetIsLocalOffset());
        }

        private bool GetIsLocal()
        {
            var span = new Span<Byte>(_preamble);
            var bytes = span.Slice(GetIsLocalOffset(), SizeOfIsLocal);
            return BitConverter.ToBoolean(bytes);
        }

        private int ComputeRowSize()
        {
            _rowSize = 0;
            // need to compute the size of the row, starting with fixed size columns, then variable columns
            throw new NotImplementedException();
        }
        #endregion

    }
}
