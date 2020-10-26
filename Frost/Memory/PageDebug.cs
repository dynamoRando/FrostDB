using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace FrostDB
{
    class PageDebug
    {
        #region Private Fields
        private Page _page;
        #endregion

        #region Public Properties
        #endregion

        #region Constructors
        public PageDebug(Page page)
        {
            _page = page;
        }
        #endregion

        /*
         * Page Byte Array Layout:
         * PageId TotalBytesUsed TotalRows - this is the page preamble
         * <rowDataStart> [row] [row] [row] [row] <rowDataEnd>
         * <rowDataEnd == [rowId = -1, IsLocal = true]>
         */


        #region Public Methods
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            ReadOnlySpan<byte> data = _page.ToSpan();

            int pageId = DatabaseBinaryConverter.BinaryToInt(data.Slice(0, DatabaseConstants.SIZE_OF_PAGE_ID));

            int totalBytesUsed = DatabaseBinaryConverter.BinaryToInt(data.Slice(_page.GetTotalBytesUsedOffset(),
                DatabaseConstants.SIZE_OF_TOTAL_BYTES_USED));

            int totalRows = DatabaseBinaryConverter.BinaryToInt(data.Slice(_page.GetTotalRowsOffset(),
                DatabaseConstants.SIZE_OF_ROW_SIZE));

            builder.Append("******* PAGE DEBUG *******");
            builder.Append(Environment.NewLine);
            builder.Append($"Page Header: PageId: {pageId.ToString()} " +
                $"TotalBytesUsed: {totalBytesUsed.ToString()} TotalRows: {totalRows.ToString()}");
            builder.Append(Environment.NewLine);
            builder.Append($"Row Data: ");
            IterateOverData(data, ref builder);
            builder.Append("******* END PAGE DEBUG *******");
            return builder.ToString();
        }

        public void Output()
        {
            Debug.WriteLine(ToString());
        }
        #endregion

        #region Private Methods
        private void IterateOverData(ReadOnlySpan<byte> data, ref StringBuilder builder)
        {
            int currentOffset = DatabaseConstants.SIZE_OF_PAGE_PREAMBLE;
            int currentRowNum = 0;

            while (currentOffset < DatabaseConstants.PAGE_SIZE)
            {
                int rowId;
                bool isLocal;
                int sizeOfRow;

                RowPreamble.Parse(data.Slice(currentOffset, DatabaseConstants.SIZE_OF_ROW_PREAMBLE), out rowId, out isLocal);

                // check for end of data row identifier
                if (isLocal && (rowId <= DatabaseConstants.END_OF_ROW_DATA_ID))
                {
                    break;
                }

                currentOffset += DatabaseConstants.SIZE_OF_ROW_PREAMBLE;

                if (isLocal)
                {
                    var values = new RowValue2[_page.Schema.Columns.Length];

                    // we need the size of the row to parse how far along we should go in the array (span).
                    // we will however not adjust the offset since the method LocalRowBodyFromBinary includes parsing the rowSize prefix (an int32 size (4 bytes)).

                    sizeOfRow = BitConverter.ToInt32(data.Slice(currentOffset, DatabaseConstants.SIZE_OF_ROW_SIZE));

                    if (sizeOfRow <= 0)
                    {
                        throw new InvalidOperationException("The size of the row was not saved on the page");
                    }

                    int rowSize; // this isn't really needed, but it's a required param of the method below
                    Row2.LocalRowBodyFromBinary(data.Slice(currentOffset, sizeOfRow), out rowSize, ref values, _page.Schema.Columns);

                    //rows.Add(new Row2(rowId, isLocal, _schema.Columns, _process.Id.Value, values, sizeOfRow));
                    var row = new RowStruct { IsLocal = isLocal, RowId = rowId, ParticipantId = Guid.Empty, RowSize = sizeOfRow, Values = values };
                    Debug.WriteLine(row.ToString());
                    currentOffset += sizeOfRow;
                    currentRowNum++;
                }
                else
                {
                    sizeOfRow = DatabaseConstants.PARTICIPANT_ID_SIZE;
                    Guid particpantId = DatabaseBinaryConverter.BinaryToGuid(data.Slice(currentOffset, sizeOfRow));
                    //rows.Add(new Row2(rowId, isLocal, particpantId, sizeOfRow, _schema.Columns));
                    var row = new RowStruct { IsLocal = isLocal, ParticipantId = particpantId, RowSize = sizeOfRow, RowId = rowId, Values = null };
                    Debug.WriteLine(row.ToString());
                    currentOffset += sizeOfRow;
                    currentRowNum++;
                }
            }
        }
        #endregion

    }
}
