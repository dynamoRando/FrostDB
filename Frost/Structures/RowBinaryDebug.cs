using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace FrostDB
{
    /*
        * Row Byte Array Layout:
        * RowId IsLocal {{SizeOfRow | ParticipantId} | RowData}
        * RowId IsLocal - preamble (used in inital load of the Row)
        * 
        * if IsLocal == true, then need to request the rest of the byte array
        * 
        * if IsLocal == false, then need to request the rest of the byte array, i.e. the size of the ParticipantId
        * 
        * SizeOfRow is the size of the rest of the row in bytes minus the preamble.  It includes the int32 byte size value itself.
        * For a remote row, this is just the size of the ParticipantId (a guid)
        * For a local row, this is the total size of all the data
        * 
        * If IsLocal == true, format is as follows -
        * [data_col1] [data_col2] [data_colX] - fixed size columns first
        * [SizeOfVar] [varData] [SizeOfVar] [varData] - variable size columns
        * [ -1 preamble] - signals the end of row data (a preamble whose RowId == -1 and IsLocal == true)
        */

    internal class RowBinaryDebug
    {
        #region Private Fields
        #endregion

        #region Public Properties
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        public static void DebugRow(ReadOnlySpan<byte> rowData, TableSchema2 schema)
        {
            StringBuilder builder = new StringBuilder();
            int currentOffset = 0;

            builder.Append($"**** ROW DEBUG ****");
            builder.Append(Environment.NewLine);

            var rowSpan = rowData.Slice(0, DatabaseConstants.SIZE_OF_ROW_ID);
            int rowId = DatabaseBinaryConverter.BinaryToInt(rowSpan);
            builder.Append($"RowId: {rowId.ToString()} ");

            currentOffset += DatabaseConstants.SIZE_OF_ROW_ID;

            var isLocalSpan = rowData.Slice(currentOffset, DatabaseConstants.SIZE_OF_IS_LOCAL);
            bool isLocal = DatabaseBinaryConverter.BinaryToBoolean(isLocalSpan);
            builder.Append($"IsLocal: {isLocal.ToString()} ");

            if (isLocal)
            {
                currentOffset += DatabaseConstants.SIZE_OF_IS_LOCAL;
                var sizeOfRowSpan = rowData.Slice(currentOffset, DatabaseConstants.SIZE_OF_ROW_SIZE);
                int sizeOfRow = DatabaseBinaryConverter.BinaryToInt(sizeOfRowSpan);

                builder.Append($"SizeOfRow: {sizeOfRow.ToString()} ");

                currentOffset += DatabaseConstants.SIZE_OF_ROW_SIZE;

                // to do: using the schema, iterate over the row data and print out
                schema.Columns.OrderByByteFormat();

                foreach (var column in schema.Columns)
                {
                    if (column.IsVariableLength)
                    {
                        // need to parse the first 4 bytes to get the size, then the data
                        ReadOnlySpan<byte> dataLengthSpan = rowData.Slice(currentOffset, DatabaseConstants.SIZE_OF_INT);
                        int dataLength = DatabaseBinaryConverter.BinaryToInt(dataLengthSpan);
                        currentOffset += DatabaseConstants.SIZE_OF_INT;
                        ReadOnlySpan<byte> data = rowData.Slice(currentOffset, dataLength);
                        RowValue2 value = column.Parse(data);
                        currentOffset += dataLength;
                        builder.Append($"{value.Column} : {value.Value} : Length {dataLength.ToString()}");
                    }
                    else
                    {
                        RowValue2 value = column.Parse(rowData.Slice(currentOffset, column.Size));
                        currentOffset += column.Size;
                        builder.Append($"{value.Column} : {value.Value} : Length {column.Size.ToString()}");
                    }
                }
            }
            else
            {
                var guidSpan = rowData.Slice(DatabaseConstants.SIZE_OF_ROW_ID + DatabaseConstants.SIZE_OF_IS_LOCAL,
                    DatabaseConstants.PARTICIPANT_ID_SIZE);
                Guid participantId = DatabaseBinaryConverter.BinaryToGuid(guidSpan);
                builder.Append($"ParticipantId: {participantId.ToString()} ");
            }


            builder.Append(Environment.NewLine);
            builder.Append($"**** END ROW DEBUG ****");

            Debug.WriteLine(builder.ToString());
        }
        #endregion

        #region Private Methods
        #endregion

    }
}
