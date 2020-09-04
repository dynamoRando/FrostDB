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
         * PageId, TableId, DatabaseId TotalBytesUsed - this is the page preamble
         * <rowDataStart> [row] [row] [row] [row] <rowDataEnd>
         */
        #region Private Fields
        private byte[] _data;
        private int _rowDataStart;
        private int _rowDataEnd;
        private PageAddress _address;
        #endregion

        #region Public Properties
        public int Id { get; set; }
        public int TableId { get; set; }
        public int DbId { get; set; }
        public int TotalBytesUsed { get; set; }
        public byte[] Data => _data;
        public int SizeOfId => DatabaseConstants.SIZE_OF_PAGE_ID;
        public int SizeOfDbId => DatabaseConstants.SIZE_OF_DB_ID;
        public int SizeOfTableId => DatabaseConstants.SIZE_OF_TABLE_ID;
        public int SizeOfBytesUsed => DatabaseConstants.SIZE_OF_TOTAL_BYTES_USED;
        public int SizeOfPagePreamble => DatabaseConstants.SIZE_OF_PAGE_PREAMBLE;
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
        /// <summary>
        /// Returns all the rows held in the page based on the specified schema
        /// </summary>
        /// <param name="schema">The table schema</param>
        /// <returns>A list of rows</returns>
        public List<Row2> GetRows(TableSchema2 schema)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a row in the page based on the specified id
        /// </summary>
        /// <param name="id">The Id of the row</param>
        /// <returns>The row</returns>
        public Row2 GetRow(int id)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        private int GetTotalBytesUsedOffset()
        {
            return SizeOfId + SizeOfTableId + SizeOfDbId;
        }
        private int GetDbOffset()
        {
            //PageId, TableId, DatabaseId TotalBytesUsed
            return SizeOfId + SizeOfTableId;
        }

        private int GetTableOffset()
        {
            //PageId, TableId, DatabaseId TotalBytesUsed
            return SizeOfId;
        }

        private int GetTotalBytesUsed()
        {
            var span = new Span<byte>(Data);
            var bytes = span.Slice(GetTotalBytesUsedOffset(), SizeOfBytesUsed);
            return BitConverter.ToInt32(bytes);
        }

        private int GetTableId()
        {
            var idSpan = new Span<byte>(Data);
            var idBytes = idSpan.Slice(GetTableOffset(), SizeOfTableId);
            return BitConverter.ToInt32(idBytes);
        }

        private void SaveTableId()
        {
            var idData = BitConverter.GetBytes(TableId);
            idData.CopyTo(_data, GetTableOffset());
        }

        private int GetDbId()
        {
            var idSpan = new Span<byte>(Data);
            var idBytes = idSpan.Slice(GetDbOffset(), SizeOfDbId);
            return BitConverter.ToInt32(idBytes);
        }

        private void SaveDbId()
        {
            var idData = BitConverter.GetBytes(DbId);
            idData.CopyTo(_data, GetDbOffset());
        }
        private void SaveId()
        {
            var idData = BitConverter.GetBytes(Id);
            idData.CopyTo(_data, 0);
        }

        private int GetId()
        {
            var idSpan = new Span<byte>(Data);
            var idBytes = idSpan.Slice(0, SizeOfId);
            return BitConverter.ToInt32(idBytes);
        }
        #endregion

    }
}
