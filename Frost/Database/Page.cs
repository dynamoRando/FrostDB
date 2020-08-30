using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace FrostDB
{
    public class Page
    {
        /*
         * Page Byte Array Layout:
         * PageId, TableId, DatabaseId
         * <rowDataStart> [row] [row] [row] [row] <rowDataEnd>
         */
        #region Private Fields
        private int _sizeOfId;
        private int _sizeOfDbId;
        private int _sizeOfTableId;
        private byte[] _data;
        private int _rowDataStart;
        private int _rowDataEnd;
        private PageAddress _address;
        #endregion

        #region Public Properties
        public int Id { get; set; }
        public int TableId { get; set; }
        public int DbId { get; set; }
        public byte[] Data => _data;
        public int SizeOfId => _sizeOfId;
        public int SizeOfDbId => _sizeOfDbId;
        public int SizeOfTableId => _sizeOfTableId;
        public int RowDataStart => _rowDataStart;
        public int RowDataEnd => _rowDataEnd;
        public PageAddress Address => _address;
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

            _address = new PageAddress { DatabaseId = DbId, TableId = TableId, PageId = Id };
        }

        public Page(byte[] data, int id, int tableId, int dbId)
        {
            Id = id;
            TableId = tableId;
            DbId = dbId;

            _address = new PageAddress { DatabaseId = DbId, TableId = TableId, PageId = Id };

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
