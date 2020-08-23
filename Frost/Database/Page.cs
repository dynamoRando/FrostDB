using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace FrostDB
{
    public class Page
    {
        /*
         * PageId, TableId, DatabaseId
         */
        #region Private Fields
        private int _sizeOfId;
        private int _sizeOfDbId;
        private int _sizeOfTableId;
        private byte[] _data;
        #endregion

        #region Public Properties
        public int Id { get; set; }
        public int TableId { get; set; }
        public int DbId { get; set; }
        public byte[] Data => _data;
        public int SizeOfId => _sizeOfId;
        public int SizeOfDbId => _sizeOfDbId;
        public int SizeOfTableId => _sizeOfTableId;
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Page() 
        {
            _data = new byte[DatabaseConstants.PAGE_SIZE];
        }
        public Page(byte[] data)
        {
            _data = data;
            Id = GetId();
            TableId = GetTableId();
            DbId = GetDbId();
        }

        public Page(byte[] data, int id, int tableId, int dbId)
        {
            Id = id;
            TableId = tableId;
            DbId = dbId;

            _data = data;
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        private int GetDbOffset()
        {
            return _sizeOfId + _sizeOfTableId;
        }

        private int GetTableOffset()
        {
            return _sizeOfId;
        }

        private int GetTableId()
        {
            var idSpan = new Span<byte>(Data);
            var idBytes = idSpan.Slice(GetTableOffset(), _sizeOfTableId);
            return BitConverter.ToInt32(idBytes);
        }

        private void SaveTableId()
        {
            var idData = BitConverter.GetBytes(TableId);
            _sizeOfTableId = idData.Length;
            idData.CopyTo(_data, GetTableOffset());
        }

        private int GetDbId()
        {
            var idSpan = new Span<byte>(Data);
            var idBytes = idSpan.Slice(GetDbOffset(), _sizeOfDbId);
            return BitConverter.ToInt32(idBytes);
        }

        private void SaveDbId()
        {
            var idData = BitConverter.GetBytes(DbId);
            _sizeOfDbId = idData.Length;
            idData.CopyTo(_data, GetDbOffset());
        }
        private void SaveId()
        {
            var idData = BitConverter.GetBytes(Id);
            _sizeOfId = idData.Length;
            idData.CopyTo(_data, 0);
        }

        private int GetId()
        {
            var idSpan = new Span<byte>(Data);
            var idBytes = idSpan.Slice(0, _sizeOfId);
            return BitConverter.ToInt32(idBytes);
        }
        #endregion

    }
}
