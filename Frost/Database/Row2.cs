using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class Row2
    {
        /*
         * Row Byte Array Layout:
         * RowId IsLocal ParticipantId
         * [data_col1] [data_col2] [data_colX]
         */
        #region Private Fields
        private byte[] _data;
        private bool _isLocal;
        private Guid _participantId;
        private List<ColumnSchema> _columns;
        private int _rowId;
        #endregion

        #region Public Properties
        public int RowId => _rowId;
        public int SizeOfParticipantId => DatabaseConstants.PARTICIPANT_ID_SIZE;
        public int SizeOfIsLocal => DatabaseConstants.SIZE_OF_IS_LOCAL;
        public int SizeOfRowId => DatabaseConstants.SIZE_OF_ROW_ID;
        public byte[] Data => _data;
        public bool IsLocal => _isLocal;
        public Guid ParticipantId => _participantId;
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Row2() { }
        public Row2(byte[] data, List<ColumnSchema> columns)
        {
            _data = data;
            _columns = columns;
            GetRowId();
            GetIsLocal();
            GetParticipantId();
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        private int GetRowIdOffset()
        {
            return 0;
        }

        private int GetIsLocalOffset()
        {
            return SizeOfRowId;
        }

        private int GetParticipantOffset()
        {
            return SizeOfRowId + SizeOfIsLocal;
        }

        private int GetRowId()
        {
            var span = new Span<byte>(Data);
            var bytes = span.Slice(0, SizeOfRowId);
            return BitConverter.ToInt32(bytes);
        }

        private void SetRow()
        {
            var data = BitConverter.GetBytes(RowId);
            data.CopyTo(_data, GetRowIdOffset());
        }

        private Guid GetParticipantId() 
        {
            var span = new Span<byte>(Data);
            var bytes = span.Slice(GetParticipantOffset(), SizeOfParticipantId);
            return new Guid(bytes);
        }

        private void SaveParticipant()
        {
            var data = ParticipantId.ToByteArray();
            data.CopyTo(_data, GetParticipantOffset());
        }

        private void SaveIsLocal()
        {
            var data = BitConverter.GetBytes(IsLocal);
            data.CopyTo(_data, GetIsLocalOffset());
        }

        private bool GetIsLocal()
        {
            var span = new Span<Byte>(Data);
            var bytes = span.Slice(GetIsLocalOffset(), SizeOfIsLocal);
            return BitConverter.ToBoolean(bytes);
        }
        #endregion

    }
}
