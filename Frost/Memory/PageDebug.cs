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
            ReadOnlySpan<byte> _data = _page.ToSpan();

            int pageId = DatabaseBinaryConverter.BinaryToInt(_data.Slice(0, DatabaseConstants.SIZE_OF_PAGE_ID));

            int totalBytesUsed = DatabaseBinaryConverter.BinaryToInt(_data.Slice(_page.GetTotalBytesUsedOffset(),
                DatabaseConstants.SIZE_OF_TOTAL_BYTES_USED));

            int totalRows = DatabaseBinaryConverter.BinaryToInt(_data.Slice(_page.GetTotalRowsOffset(),
                DatabaseConstants.SIZE_OF_ROW_SIZE));

            builder.Append("******* PAGE DEBUG *******");
            builder.Append(Environment.NewLine);
            builder.Append($"Page Header: PageId: {pageId.ToString()} " +
                $"TotalBytesUsed: {totalBytesUsed.ToString()} TotalRows: {totalRows.ToString()}");
            builder.Append(Environment.NewLine);
            builder.Append("******* END PAGE DEBUG *******");
            return builder.ToString();
        }

        public void Output()
        {
            Debug.WriteLine(ToString());
        }
        #endregion

        #region Private Methods
        #endregion

    }
}
