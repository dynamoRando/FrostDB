using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Linq;
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
         * PageId TotalBytesUsed TotalRows - this is the page preamble
         * <rowDataStart> [row] [row] [row] [row] <rowDataEnd>
         * <rowDataEnd == [rowId = -1, IsLocal = true]>
         */
        #region Private Fields
        // consider locks around _data
        private byte[] _data;
        private int _rowDataStart;
        private int _rowDataEnd;
        private int _id;
        private int _tableId;
        private int _dbId;
        private int _totalBytesUsed;
        // consider wrapping _totalRows around a lock, and having get/set private functions. Consider saving the value back to _data every time it is modified.
        private int _totalRows;
        private PageAddress _address;
        private BTreeContainer _container;
        private TableSchema2 _schema;
        private Process _process;
        private List<Guid> _pendingXacts;
        private PageDebug _debug;
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
        public int SizeOfTotalRows => DatabaseConstants.SIZE_OF_PAGE_NUM_ROWS;
        public int RowDataStart => _rowDataStart;
        public int RowDataEnd => _rowDataEnd;
        public PageAddress Address => _address;
        public int TotalRows => _totalRows;

        /// <summary>
        /// The list of xacts that were applied to this page
        /// </summary>
        public List<Guid> PendingXacts => _pendingXacts;

        /// <summary>
        /// Indicates that the page has xacts applied to it, thus needing to save the entire page to disk
        /// </summary>
        public bool IsPendingReconciliation => PendingReconciliation();

        public TableSchema2 Schema => _schema;
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

            _pendingXacts = new List<Guid>();
            _debug = new PageDebug(this);

            Debug.WriteLine($"Constructing new page in memory with id: {_id.ToString()}");
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
            _totalRows = GetTotalRows();

            _address = new PageAddress { DatabaseId = databaseId, TableId = tableId, PageId = Id };

            SetPreamble(false);

            _pendingXacts = new List<Guid>();
            _debug = new PageDebug(this);

            Debug.WriteLine($"Constructing new page from disk with id: {_id.ToString()}");
        }

        public Page(byte[] data, BTreeAddress address) : this(data, address.TableId, address.DatabaseId)
        {
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Return this page's binary data
        /// </summary>
        /// <returns>The binary array data for this page</returns>
        public byte[] ToBinary()
        {
            return _data;
        }

        public ReadOnlySpan<byte> ToSpan()
        {
            return new ReadOnlySpan<byte>(_data);
        }

        /// <summary>
        /// Clears the pending reconciliation list and marks the Page as reconciled against disk
        /// </summary>
        public void MarkAsReconciled()
        {
            _pendingXacts.Clear();
        }

        public int GetMaxRowId()
        {
            int maxRowId = 0;

            // rent 1
            RowStruct[] rows = RentRowStructArrayFromPool(_totalRows);
            IterateOverData(ref rows);
            if (rows.Length != 0)
            {
                maxRowId = rows.Max(row => row.RowId);
            }

            // return 1
            ReturnRowStructArrayToPool(ref rows);

            return maxRowId;
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
        /// Attempts to add a row to this page. By doing this, this will mark the page as pending reconciliation (to disk).
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
            RentedByteArray preamble = RentByteArrayFromPool(DatabaseConstants.SIZE_OF_ROW_PREAMBLE);
            Row2.BuildRowPreamble(ref preamble, rowId, !row.IsReferenceInsert);

            RentedByteArray rowData;
            if (row.IsReferenceInsert)
            {
                // need to save off the GUID id

                // rent 2
                rowData = RentByteArrayFromPool(DatabaseConstants.PARTICIPANT_ID_SIZE);
                row.ToBinaryFormat(ref rowData);
            }
            else
            {
                // need to save off the size of the row + all the actual row data

                // rent 2
                rowData = RentByteArrayFromPool(row.Size + DatabaseConstants.SIZE_OF_ROW_SIZE);
                row.ToBinaryFormat(ref rowData);
            }

            // rent 3
            RentedByteArray totalRowData = RentByteArrayFromPool(preamble.RequestedSize + rowData.RequestedSize);

            // combine the preamble and the row data together
            Array.Copy(preamble.Array, 0, totalRowData.Array, 0, preamble.RequestedSize);
            Array.Copy(rowData.Array, 0, totalRowData.Array, preamble.RequestedSize, rowData.RequestedSize);

            Debug.WriteLine($"Total Row Length: {totalRowData.RequestedSize.ToString()}");

            RowBinaryDebug.DebugRow(new ReadOnlySpan<byte>(totalRowData.Array));

            // to do: add totalRowData to this Page's data
            if (CanInsertNewRow(totalRowData.RequestedSize))
            {
                Array.Copy(totalRowData.Array, 0, _data, GetNextAvailableRowOffset(), totalRowData.RequestedSize);
                _totalBytesUsed += totalRowData.RequestedSize;
                _totalRows++;
                SaveTotalRows();
                SaveTotalBytesUsed();
                result = true;
            }
            else
            {
                // we need to add this row to the next page because we are out of room on this page
                // throw exception here or... ?
            }

            _pendingXacts.Add(row.XactId);

            // return 1, 2, 3
            ReturnByteArrayToPool(ref preamble);
            ReturnByteArrayToPool(ref rowData);
            ReturnByteArrayToPool(ref totalRowData);

            _debug.Output();

            return result;
        }

        /// <summary>
        /// Returns all the rows held in the page based on the specified schema
        /// </summary>
        /// <param name="schema">The table schema</param>
        /// <returns>A list of rows</returns>
        public RowStruct[] GetRows(TableSchema2 schema)
        {
            var rows = new RowStruct[_totalRows];
            IterateOverData(ref rows);
            return rows;
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

        public int GetTotalBytesUsedOffset()
        {
            return SizeOfId;
        }

        public int GetTotalRowsOffset()
        {
            return SizeOfId + SizeOfBytesUsed;
        }
        #endregion

        #region Private Methods
        private void SetPreamble(bool isBrandNewPage)
        {
            SetId();
            SetTotalRows(isBrandNewPage);
            SetTotalBytesUsed(isBrandNewPage);
        }

        /// <summary>
        /// Saves the page's field _id to _data
        /// </summary>
        private void SetId()
        {
            var idData = BitConverter.GetBytes(_id);
            idData.CopyTo(_data, 0);
        }

        /// <summary>
        /// Gets the id from the page's _data 
        /// </summary>
        /// <returns></returns>
        private int GetId()
        {
            var idSpan = new Span<byte>(_data);
            var idBytes = idSpan.Slice(0, SizeOfId);
            return BitConverter.ToInt32(idBytes);
        }

        /// <summary>
        /// Attempts to read from _data the total number of rows on this page
        /// </summary>
        /// <returns>The total number of rows saved in _data array</returns>
        private int GetTotalRows()
        {
            var span = new Span<byte>(_data);
            var bytes = span.Slice(GetTotalRowsOffset(), SizeOfTotalRows);
            return BitConverter.ToInt32(bytes);
        }

        /// <summary>
        /// Saves the page's total rows to _data and assigns to field _totalrows.
        /// If brand new page is true, will default to 0 and save to _data and assign to _totalrows.
        /// If brand new page is false, will read from _data and assign to _totalRows.
        /// </summary>
        /// <param name="isBrandNewPage"></param>
        private void SetTotalRows(bool isBrandNewPage)
        {
            if (isBrandNewPage)
            {
                _totalRows = 0;
                SaveTotalRows();
            }
            else
            {
                _totalRows = GetTotalRows();
            }
        }

        /// <summary>
        /// Saves the total bytes used field _totalBytesUsed to this page's _data field.
        /// </summary>
        private void SaveTotalBytesUsed()
        {
            byte[] item = BitConverter.GetBytes(_totalBytesUsed);
            Array.Copy(item, 0, _data, GetTotalBytesUsedOffset(), item.Length);
        }

        /// <summary>
        /// Saves _totalRows to this page's _data
        /// </summary>
        private void SaveTotalRows()
        {
            byte[] item = BitConverter.GetBytes(_totalRows);
            Array.Copy(item, 0, _data, GetTotalRowsOffset(), item.Length);
        }

        /// <summary>
        /// Saves the page's total bytes used to _data and assigns to field _totalBytesUsed. 
        /// If a brand new page, defaults to 0. If loading from disk, iterates over _data and computes the total bytes
        /// and then assigns to _totalBytesUsed and saves to _data.
        /// </summary>
        /// <param name="isBrandNewPage">True if this is a new page, otherwise false if loading from disk</param>
        private void SetTotalBytesUsed(bool isBrandNewPage)
        {
            if (isBrandNewPage)
            {
                _totalBytesUsed = 0;
                SaveTotalBytesUsed();
            }
            else
            {
                // rent 1
                RowStruct[] rows = RentRowStructArrayFromPool(_totalRows);
                IterateOverData(ref rows);
                _totalBytesUsed = rows.Sum(row => row.RowSize);

                // copy _totalBytesUsed to it's location in _data [in the Page preamble]
                SaveTotalBytesUsed();

                // return 1
                ReturnRowStructArrayToPool(ref rows);
            }
        }

        /// <summary>
        /// Rents a RowStruct[] from the ArrayPool. You should make sure to return the array back to the pool when finished.
        /// </summary>
        /// <param name="arraySize">The size of the array to rent</param>
        /// <returns>An array of specified size from the pool</returns>
        private static RowStruct[] RentRowStructArrayFromPool(int arraySize)
        {
            return ArrayPool<RowStruct>.Shared.Rent(arraySize);
        }

        /// <summary>
        /// Returns a RowStruct[] to the ArrayPool
        /// </summary>
        /// <param name="array">The array to return to the ArrayPool</param>
        /// <param name="shouldClean">True if the ArrayPool should clear the contents of the array before returning to the pool, otherwise false.</param>
        private static void ReturnRowStructArrayToPool(ref RowStruct[] array, bool shouldClean = true)
        {
            ArrayPool<RowStruct>.Shared.Return(array, shouldClean);
        }

        /// <summary>
        /// Rents a byte array rented from the ArrayPool. You should make sure to return the array back to the pool when finished.
        /// </summary>
        /// <param name="arraySize">The size of the array to rent</param>
        /// <returns>An array of specified size from the ArrayPool</returns>
        private static RentedByteArray RentByteArrayFromPool(int arraySize)
        {
            var item = new RentedByteArray();
            item.Array = ArrayPool<byte>.Shared.Rent(arraySize);
            item.RequestedSize = arraySize;
            return item;
        }

        /// <summary>
        /// Returns a rented byte array to the ArrayPool
        /// </summary>
        /// <param name="array">The array to return to the ArrayPool</param>
        /// <param name="shouldClean">True if the ArrayPool should clear the contents of the array before returning to the pool, otherwise false.</param>
        private static void ReturnByteArrayToPool(ref byte[] array, bool shouldClean = true)
        {
            ArrayPool<byte>.Shared.Return(array, shouldClean);
        }

        /// <summary>
        /// Returns a rented byte array to the ArrayPool
        /// </summary>
        /// <param name="array">The array to return to the ArrayPool</param>
        /// <param name="shouldClean">True if the ArrayPool should clear the contents of the array before returning to the pool, otherwise false.</param>
        private static void ReturnByteArrayToPool(ref RentedByteArray array, bool shouldClean = true)
        {
            ArrayPool<byte>.Shared.Return(array.Array, shouldClean);
        }

        private void InitalizeDataWithEndOfRowData()
        {
            byte[] endOfRowId = BitConverter.GetBytes(DatabaseConstants.END_OF_ROW_DATA_ID);
            byte[] isLocalId = BitConverter.GetBytes(true);
            int currentOffset = DatabaseConstants.SIZE_OF_PAGE_PREAMBLE;

            // rent 1
            byte[] endofDataPreamble = RentByteArrayFromPool(endOfRowId.Length + isLocalId.Length).Array;
            Array.Copy(endOfRowId, endofDataPreamble, endOfRowId.Length);
            Array.Copy(isLocalId, 0, endofDataPreamble, endOfRowId.Length, isLocalId.Length);

            while ((currentOffset < DatabaseConstants.PAGE_SIZE) && (endofDataPreamble.Length + currentOffset) < DatabaseConstants.PAGE_SIZE)
            {
                Array.Copy(endofDataPreamble, 0, _data, currentOffset, endofDataPreamble.Length);
                currentOffset += endofDataPreamble.Length;
            }

            // return 1
            ReturnByteArrayToPool(ref endofDataPreamble);
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
        /// Iterates over this page's _data and populates the supplied array with structs representing the rows
        /// </summary>
        /// <param name="rows">An array of RowStruct to populate</param>
        private void IterateOverData(ref RowStruct[] rows)
        {
            var dataSpan = new ReadOnlySpan<byte>(_data);
            int currentOffset = DatabaseConstants.SIZE_OF_PAGE_PREAMBLE;
            int currentRowNum = 0;

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

                if (currentRowNum >= _totalRows)
                {
                    break;
                }

                currentOffset += DatabaseConstants.SIZE_OF_ROW_PREAMBLE;

                if (isLocal)
                {
                    var values = new RowValue2[_schema.Columns.Length];

                    // we need the size of the row to parse how far along we should go in the array (span).
                    // we will however not adjust the offset since the method LocalRowBodyFromBinary includes parsing the rowSize prefix (an int32 size (4 bytes)).

                    sizeOfRow = BitConverter.ToInt32(dataSpan.Slice(currentOffset, DatabaseConstants.SIZE_OF_ROW_SIZE));

                    if (sizeOfRow <= 0)
                    {
                        throw new InvalidOperationException("The size of the row was not saved on the page");
                    }

                    int rowSize; // this isn't really needed, but it's a required param of the method below
                    Row2.LocalRowBodyFromBinary(dataSpan.Slice(currentOffset, sizeOfRow), out rowSize, ref values, _schema.Columns);

                    //rows.Add(new Row2(rowId, isLocal, _schema.Columns, _process.Id.Value, values, sizeOfRow));
                    rows[currentRowNum] = new RowStruct { IsLocal = isLocal, RowId = rowId, ParticipantId = Guid.Empty, RowSize = sizeOfRow, Values = values };
                    currentOffset += sizeOfRow;
                    currentRowNum++;
                }
                else
                {
                    sizeOfRow = DatabaseConstants.PARTICIPANT_ID_SIZE;
                    Guid particpantId = DatabaseBinaryConverter.BinaryToGuid(dataSpan.Slice(currentOffset, sizeOfRow));
                    //rows.Add(new Row2(rowId, isLocal, particpantId, sizeOfRow, _schema.Columns));
                    rows[currentRowNum] = new RowStruct { IsLocal = isLocal, ParticipantId = particpantId, RowSize = sizeOfRow, RowId = rowId, Values = null };
                    currentOffset += sizeOfRow;
                    currentRowNum++;
                }
            }
        }

        /// <summary>
        /// Determines if this page has xacts applied to it. If true, means the page needs to be saved to disk.
        /// </summary>
        /// <returns>True if pending save to disk, otherwise false</returns>
        private bool PendingReconciliation()
        {
            return _pendingXacts.Count > 0;
        }
        #endregion

    }
}
