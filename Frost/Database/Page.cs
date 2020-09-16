﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Buffers;
using Antlr4.Runtime.Atn;
using log4net.Util;
using System.Data;
using System.Linq;

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
         * <rowDataEnd == [rowId = -1, IsLocal = true]>
         */
        #region Private Fields
        private byte[] _data;
        private int _rowDataStart;
        private int _rowDataEnd;
        private int _id;
        private int _tableId;
        private int _dbId;
        private int _totalBytesUsed;
        private int _totalRows;
        private PageAddress _address;
        private BTreeContainer _container;
        private TableSchema2 _schema;
        private Process _process;
        #endregion

        #region Public Properties
        public int Id => _id;
        public int TableId => _tableId;
        public int DbId => _dbId;
        /// <summary>
        /// How many bytes, excluding the page preamble, are dedicated to row data
        /// </summary>
        public int TotalBytesUsed => _totalBytesUsed;
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
        public Page(int id, int tableId, int dbId, TableSchema2 schema, Process process)
        {
            _id = id;
            _tableId = tableId;
            _dbId = dbId;
            _address = new PageAddress { DatabaseId = DbId, TableId = TableId, PageId = Id };
            _data = new byte[DatabaseConstants.PAGE_SIZE];
            _totalRows = 0;
            _schema = schema;
            _process = process;

            SetPreamble(true);
            InitalizeDataWithEndOfRowData();
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
            _tableId = tableId;
            _dbId = databaseId;
            _id = GetId();

            _address = new PageAddress { DatabaseId = databaseId, TableId = tableId, PageId = Id };

            SetPreamble(false);
        }

        #endregion

        #region Public Methods
        public int GetMaxRowId()
        {
            var dataSpan = new Span<byte>(_data);
            int currentOffset = DatabaseConstants.SIZE_OF_PAGE_PREAMBLE;
            int maxRowId = 0;

            int rowId;
            bool isLocal;

            RowPreamble.Parse(dataSpan.Slice(currentOffset, DatabaseConstants.SIZE_OF_ROW_PREAMBLE), out rowId, out isLocal);

            while (currentOffset < DatabaseConstants.PAGE_SIZE)
            {

                if (rowId > maxRowId)
                {
                    maxRowId = rowId;
                }
                // parse the next rows, etc.
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Determines if the next available row offset + the specified row length is less than the total page size
        /// </summary>
        /// <param name="rowLength">The length of the row you wish to insert</param>
        /// <returns>True if there is room left on the page, otherwise false</returns>
        public bool CanInsertRow(int lengthOfRow)
        {
            return CanInsertNewRow(lengthOfRow);
        }

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
            byte[] preamble = RentFromPool(DatabaseConstants.SIZE_OF_ROW_PREAMBLE);
            Row2.BuildRowPreamble(ref preamble, rowId, !row.IsReferenceInsert);

            byte[] rowData = null;
            if (row.IsReferenceInsert)
            {
                // need to save off the GUID id

                // rent 2
                rowData = RentFromPool(DatabaseConstants.PARTICIPANT_ID_SIZE);
                row.ToBinaryFormat(ref rowData);
            }
            else
            {
                // need to save off the size of the row + all the actual row data

                // rent 2
                rowData = RentFromPool(row.Size + DatabaseConstants.SIZE_OF_ROW_SIZE);
                row.ToBinaryFormat(ref rowData);
            }

            // rent 3
            byte[] totalRowData = RentFromPool(preamble.Length + rowData.Length);

            // combine the preamble and the row data together
            Array.Copy(preamble, 0, totalRowData, 0, preamble.Length);
            Array.Copy(rowData, 0, totalRowData, preamble.Length, rowData.Length);

            // to do: add totalRowData to this Page's data
            if (CanInsertNewRow(totalRowData.Length))
            {
                Array.Copy(totalRowData, 0, _data, GetNextAvailableRowOffset(), totalRowData.Length);
                _totalBytesUsed += totalRowData.Length;
                _totalRows++;
                result = true;
            }
            else
            {
                // we need to add this row to the next page because we are out of room on this page
                // throw exception here or... ?
            }

            // to do: reconcile the xact on disk
            throw new NotImplementedException();

            // return 1, 2, 3
            ReturnToPool(ref preamble);
            ReturnToPool(ref rowData);
            ReturnToPool(ref totalRowData);

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
        private void SetPreamble(bool isBrandNewPage)
        {
            SetId();
            SetTotalBytesUsed(isBrandNewPage);
        }

        private int GetTotalBytesUsedOffset()
        {
            return SizeOfId;
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

        private void SetTotalBytesUsed(bool isBrandNewPage)
        {
            if (isBrandNewPage)
            {
                _totalBytesUsed = 0;
                byte[] totalBytesUsedArray = BitConverter.GetBytes(_totalBytesUsed);
                Array.Copy(totalBytesUsedArray, 0, _data, GetTotalBytesUsedOffset(), totalBytesUsedArray.Length);
                _totalRows = 0;
            }
            else
            {
                _totalBytesUsed = IterateOverData().Sum(row => row.RowSize);

                // copy _totalBytesUsed to it's location in _data [in the Page preamble]
                byte[] totalBytesUsedArray = BitConverter.GetBytes(_totalBytesUsed);
                Array.Copy(totalBytesUsedArray, 0, _data, GetTotalBytesUsedOffset(), totalBytesUsedArray.Length);
            }
        }

        /// <summary>
        /// Returns a byte array rented from the ArrayPool. You should make sure to return the array back to the pool when finished.
        /// </summary>
        /// <param name="arraySize">The size of the array to rent</param>
        /// <returns>An array of specified size from the ArrayPool</returns>
        private static byte[] RentFromPool(int arraySize)
        {
            return ArrayPool<byte>.Shared.Rent(arraySize);
        }

        /// <summary>
        /// Returns a rented byte array to the ArrayPool
        /// </summary>
        /// <param name="array">The array to return to the ArrayPool</param>
        /// <param name="shouldClean">True if the ArrayPool should clear the contents of the array before returning to the pool, otherwise false.</param>
        private static void ReturnToPool(ref byte[] array, bool shouldClean = true)
        {
            ArrayPool<byte>.Shared.Return(array, shouldClean);
        }

        private void InitalizeDataWithEndOfRowData()
        {
            byte[] endOfRowId = BitConverter.GetBytes(DatabaseConstants.END_OF_ROW_DATA_ID);
            byte[] isLocalId = BitConverter.GetBytes(true);
            int currentOffset = DatabaseConstants.SIZE_OF_PAGE_PREAMBLE;

            // rent 1
            byte[] endofDataPreamble = RentFromPool(endOfRowId.Length + isLocalId.Length);
            Array.Copy(endOfRowId, endofDataPreamble, endOfRowId.Length);
            Array.Copy(isLocalId, 0, endofDataPreamble, endOfRowId.Length, isLocalId.Length);

            while (currentOffset < DatabaseConstants.PAGE_SIZE)
            {
                Array.Copy(endofDataPreamble, 0, _data, currentOffset, endofDataPreamble.Length);
                currentOffset += endofDataPreamble.Length;
            }

            // return 1
            ReturnToPool(ref endofDataPreamble);
        }

        /// <summary>
        /// Computes the next available row location to insert based off of _totalBytesUsed + the size of the page preamble
        /// </summary>
        /// <returns>The index where a new row can be added</returns>
        private int GetNextAvailableRowOffset()
        {
            return _totalBytesUsed + DatabaseConstants.SIZE_OF_PAGE_PREAMBLE;
        }

        /// <summary>
        /// Determines if the next available row offset + the specified row length is less than the total page size
        /// </summary>
        /// <param name="rowLength">The length of the row you wish to insert</param>
        /// <returns>True if there is room left on the page, otherwise false</returns>
        private bool CanInsertNewRow(int rowLength)
        {
            return (rowLength + GetNextAvailableRowOffset() < DatabaseConstants.PAGE_SIZE);
        }

        /// <summary>
        /// Iterates over the page's _data and returns a list of corresponding Row2 objects
        /// </summary>
        /// <param name="rows"></param>
        private List<Row2> IterateOverData()
        {
            var rows = new List<Row2>();
            var dataSpan = new Span<byte>(_data);
            int currentOffset = DatabaseConstants.SIZE_OF_PAGE_PREAMBLE;

            while (currentOffset < DatabaseConstants.PAGE_SIZE)
            {
                int rowId;
                bool isLocal;
                int sizeOfRow;

                RowPreamble.Parse(dataSpan.Slice(currentOffset, DatabaseConstants.SIZE_OF_ROW_PREAMBLE), out rowId, out isLocal);

                // check for end of data row identifier
                if (isLocal && rowId == DatabaseConstants.END_OF_ROW_DATA_ID)
                {
                    break;
                }

                currentOffset += DatabaseConstants.SIZE_OF_ROW_PREAMBLE;

                if (isLocal)
                {
                    var values = new List<RowValue2>();
                    
                    // we need the size of the row to parse how far along we should go in the array (span).
                    // we will however not adjust the offset since the method LocalRowBodyFromBinary includes parsing the rowSize prefix (an int32 size (4 bytes)).

                    sizeOfRow = BitConverter.ToInt32(dataSpan.Slice(currentOffset, DatabaseConstants.SIZE_OF_ROW_SIZE));
                    int rowSize; // this isn't really needed, but it's a required param of the method below
                    Row2.LocalRowBodyFromBinary(dataSpan.Slice(currentOffset, sizeOfRow), out rowSize, ref values, _schema.Columns);

                    rows.Add(new Row2(rowId, isLocal, _schema.Columns, _process.Id.Value, values, sizeOfRow));
                    currentOffset += sizeOfRow;
                }
                else
                {
                    sizeOfRow = DatabaseConstants.PARTICIPANT_ID_SIZE;
                    Guid particpantId = DatabaseBinaryConverter.BinaryToGuid(dataSpan.Slice(currentOffset, sizeOfRow));
                    rows.Add(new Row2(rowId, isLocal, particpantId, sizeOfRow, _schema.Columns));
                    currentOffset += sizeOfRow;
                }
            }
            return rows;
        }
        #endregion

    }
}
