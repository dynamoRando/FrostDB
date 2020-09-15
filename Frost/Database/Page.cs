using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Buffers;
using Antlr4.Runtime.Atn;

namespace FrostDB
{
    // note: should the page be the only object that really works with byte[]? in other words, should this output only objects such as Row2?
    // and intake only RowInsert / RowUpdate / RowDelete objects?
    public class Page
    {
        /*
         * Page Byte Array Layout:
         * PageId TotalBytesUsed - this is the page preamble
         * <rowDataStart> [row] [row] [row] [row] <rowDataEnd>
         */
        #region Private Fields
        private byte[] _data;
        private int _rowDataStart;
        private int _rowDataEnd;
        private PageAddress _address;
        private BTreeContainer _container;
        #endregion

        #region Public Properties
        public int Id { get; set; }
        public int TableId { get; set; }
        public int DbId { get; set; }
        public int TotalBytesUsed { get; set; }
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
        /// <summary>
        /// Constructs a brand new binary Page object with a size based on the DatabaseConstants file. Use this constructor when creating
        /// a brand new page (i.e. not loading from disk)
        /// </summary>
        /// <param name="id">The id of this page</param>
        /// <param name="tableId">The table id that this page belongs to</param>
        /// <param name="dbId">The database id that this page belongs to</param>
        public Page(int id, int tableId, int dbId)
        {
            Id = id;
            TableId = tableId;
            DbId = dbId;

            _address = new PageAddress { DatabaseId = DbId, TableId = TableId, PageId = Id };

            _data = new byte[DatabaseConstants.PAGE_SIZE];

            SetPreamble();
        }

        /// <summary>
        /// Cosntructs a binary Page object with the specified binrary data array, and sets the TableId and the Database Id for the page.
        /// Use this constructor when loading the binary data from disk.
        /// </summary>
        /// <param name="data">The binary data</param>
        /// <param name="tableId">The table id that this page belongs to</param>
        /// <param name="databaseId">The db id that this page belongs to</param>
        public Page(byte[] data, int tableId, int databaseId)
        {
            _data = data;
            TableId = tableId;
            DbId = databaseId;
            Id = GetId();

            _address = new PageAddress { DatabaseId = databaseId, TableId = tableId, PageId = Id };

            SetPreamble();
        }

        #endregion

        #region Public Methods
        public void SetContainer(BTreeContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// Attempts to add a row to this page and then reconcile the xact on disk
        /// </summary>
        /// <param name="row">The row to be added</param>
        /// <param name="rowId">The id for this row. This should be the next available rowId</param>
        /// <returns>True if succesful, otherwise false</returns>
        public bool AddRow(RowInsert row, int rowId)
        {
            bool result = false;

            if (row == null)
            {
                throw new ArgumentNullException(nameof(row));
            }

            row.OrderByByteFormat();

            // rent 1
            byte[] preamble = ArrayPool<byte>.Shared.Rent(DatabaseConstants.SIZE_OF_ROW_PREAMBLE);
            Row2.BuildRowPreamble(ref preamble, rowId, !row.IsReferenceInsert);

            byte[] rowData = null;
            if (row.IsReferenceInsert)
            {
                // need to save off the GUID id

                // rent 2
                rowData = ArrayPool<byte>.Shared.Rent(DatabaseConstants.PARTICIPANT_ID_SIZE);
                row.ToBinaryFormat(ref rowData);
            }
            else
            {
                // need to save off the size of the row + all the actual row data

                // rent 2
                rowData = ArrayPool<byte>.Shared.Rent(row.Size + DatabaseConstants.SIZE_OF_INT);
                row.ToBinaryFormat(ref rowData);
            }

            // rent 3
            byte[] totalRowData = ArrayPool<byte>.Shared.Rent(preamble.Length + rowData.Length);

            // combine the preamble and the row data together
            Array.Copy(preamble, 0, totalRowData, 0, preamble.Length);
            Array.Copy(rowData, 0, totalRowData, preamble.Length, rowData.Length);

            // to do: add totalRowData to this Page's data

            // to do: reconcile the xact on disk
            throw new NotImplementedException();

            // return 1, 2, 3
            ArrayPool<byte>.Shared.Return(preamble, true);
            ArrayPool<byte>.Shared.Return(rowData, true);
            ArrayPool<byte>.Shared.Return(totalRowData, true);

            return result;
        }

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
        private void SetPreamble()
        {
            SetId();
            SetTotalBytesUsed();
        }

        private int GetTotalBytesUsedOffset()
        {
            return SizeOfId;
        }
      
        private int GetTotalBytesUsed()
        {
            var span = new Span<byte>(_data);
            var bytes = span.Slice(GetTotalBytesUsedOffset(), SizeOfBytesUsed);
            return BitConverter.ToInt32(bytes);
        }

        private void SetId()
        {
            var idData = BitConverter.GetBytes(Id);
            idData.CopyTo(_data, 0);
        }

        private int GetId()
        {
            var idSpan = new Span<byte>(_data);
            var idBytes = idSpan.Slice(0, SizeOfId);
            return BitConverter.ToInt32(idBytes);
        }

        private void SetTotalBytesUsed()
        {
            // needs to count all the rows (get the premables and get size from each)
            throw new NotImplementedException();
        }

        #endregion

    }
}
