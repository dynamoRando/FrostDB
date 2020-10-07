using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{

    /*
     * 
     * from SetTotalBytesUsed --
     * var dataSpan = new Span<byte>(_data);

                    // how far are we in _data;
                    int currentOffset = DatabaseConstants.SIZE_OF_PAGE_PREAMBLE;

                    // total size of all the rows we have parsed
                    int runningTotalRowSize = 0;

                    // size of the current row
                    int sizeOfRow = 0;


                    /*
                    * Page Byte Array Layout:
                    * PageId TotalBytesUsed - this is the page preamble
                    * <rowDataStart> [row] [row] [row] [row] <rowDataEnd>
                    */

    /*
     * 
     *  
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
    * [ -1 preamble] - signals the end of row data (a preamble whose RowId == -1 and IsLocal == true)
    */

    // iterate over the page binary data until we find the end of data row identifier
    //while (currentOffset < DatabaseConstants.PAGE_SIZE)
    //{
    //    int rowId;
    //    bool isLocal;

    //    RowPreamble.Parse(dataSpan.Slice(currentOffset, DatabaseConstants.SIZE_OF_ROW_PREAMBLE), out rowId, out isLocal);

    //    // check for end of data row identifier
    //    if (isLocal && rowId == DatabaseConstants.END_OF_ROW_DATA_ID)
    //    {
    //        break;
    //    }

    //    if (isLocal)
    //    {
    //        currentOffset += DatabaseConstants.SIZE_OF_ROW_PREAMBLE;
    //        sizeOfRow = BitConverter.ToInt32(dataSpan.Slice(currentOffset, DatabaseConstants.SIZE_OF_ROW_SIZE));
    //    }
    //    else
    //    {
    //        sizeOfRow = DatabaseConstants.PARTICIPANT_ID_SIZE;
    //    }

    //    // add back the size of the row preamble in addition to the row data so that we know the total bytes used in the page
    //    sizeOfRow += DatabaseConstants.SIZE_OF_ROW_PREAMBLE;

    //    runningTotalRowSize += sizeOfRow;
    //    currentOffset += sizeOfRow;
    //}

    //_totalBytesUsed = runningTotalRowSize;

}
