using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Text;

namespace FrostDB
{
    // to do: is the purpose of this class to hold byte arrays? or is this class to hold the values for read/informational purposes?
    // if the latter, it needs to have List<RowValues2> as a property and we do not need to initalize it with arrays.
    public class Row2
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

        #region Private Fields
        private RowPreamble _preamble;
        private byte[] _data;
        private Guid _participantId;
        private List<ColumnSchema> _columns;
        private int _rowSize;
        private PageAddress _pageAddress;
        private List<RowValue2> _values;
        #endregion

        #region Public Properties
        public int RowId => _preamble.RowId;
        public int SizeOfParticipantId => DatabaseConstants.PARTICIPANT_ID_SIZE;
        public int SizeOfRowSize => DatabaseConstants.SIZE_OF_ROW_SIZE;
        public int RowSize => GetRowSize();
        public bool IsLocal => _preamble.IsLocal;
        public Guid ParticipantId => _participantId;
        public PageAddress PageAddress => _pageAddress;
        public byte[] BinaryData => _data;
        public RowPreamble Preamble => _preamble;
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs a full blown Row2 object from supplied values (does not try to parse from binary array)
        /// </summary>
        /// <param name="rowId">The row id</param>
        /// <param name="isLocal">If the row is local or remote</param>
        /// <param name="columns">The column schema</param>
        /// <param name="participantId">The participant id</param>
        /// <param name="values">A list of row values</param>
        /// <param name="rowSize">The byte size of the data (does not include the preamble)</param>
        public Row2(int rowId, bool isLocal, List<ColumnSchema> columns, Guid participantId, List<RowValue2> values, int rowSize)
        {
            _preamble = new RowPreamble(rowId, isLocal);
            _columns = columns;
            _participantId = participantId;
            _values = values;
            _rowSize = rowSize;
        }

        /// <summary>
        /// Constructs a full blown Row2 object from supplied items withou the values (usually used for a reference row i.e. not local)
        /// </summary>
        /// <param name="rowId">The row id</param>
        /// <param name="isLocal">If the row is local or remote</param>
        /// <param name="columns">The column schema</param>
        /// <param name="participantId">The participant id</param>
        /// <param name="rowSize">The byte size of the data (does not include the preamble)</param>
        public Row2(int rowId, bool isLocal, Guid participantId, int rowSize, List<ColumnSchema> columns)
        {
            _preamble = new RowPreamble(rowId, isLocal);
            _columns = columns;
            _participantId = participantId;
            _rowSize = rowSize;
        }

        /// <summary>
        /// Constructs a Row2 object based on the binary preamble and the specified column schema. The column schema will be used to 
        /// parse the row binary data.
        /// </summary>
        /// <param name="preamble">The binrary preamble</param>
        /// <param name="columns">The column schema of the row</param>
        public Row2(byte[] preamble, List<ColumnSchema> columns)
        {
            _preamble = new RowPreamble(preamble);
            _columns = columns;
        }

        public Row2(Span<byte> preamble, List<ColumnSchema> columns)
        {
            _preamble = new RowPreamble(preamble);
            _columns = columns;
        }

        /// <summary>
        /// Constructs a Row2 object along with setting it's page address. 
        /// </summary>
        /// <param name="pageAddress">The page address of the row</param>
        /// <param name="preamble">The binary preamble of the row</param>
        /// <param name="columns">The column schema of the row</param>
        public Row2(PageAddress pageAddress, byte[] preamble, List<ColumnSchema> columns) : this(preamble, columns)
        {
            _pageAddress = pageAddress;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Sets the row data from a byte array. This data does not include the row preamble.
        /// </summary>
        /// <param name="data">The byte array representing the row data.</param>
        public void SetRowData(byte[] data)
        {
            _data = data;
            SetSizeOfRow();
        }

        public void SetRowData(RowInsert row)
        {
            row.Values.ToBinaryFormat();
        }


        public static void LocalRowBodyFromBinary(ReadOnlySpan<byte> span, out int sizeOfRow, ref RowValue2[] values, ColumnSchema[] schema)
        {
            int colIdx = 0;
            int currentOffset = 0;
            RowValue2 item = null;

            schema.OrderByByteFormat();

            sizeOfRow = BitConverter.ToInt32(span.Slice(0, DatabaseConstants.SIZE_OF_ROW_SIZE));

            currentOffset += DatabaseConstants.SIZE_OF_ROW_SIZE;

            foreach (var column in schema)
            {
                if (!column.IsVariableLength)
                {
                    item = column.Parse(span.Slice(currentOffset, column.Size));
                    values[colIdx] = item;
                    colIdx++;
                    currentOffset += column.Size;
                }
                else
                {
                    int sizeOfValue = DatabaseBinaryConverter.BinaryToInt(span.Slice(currentOffset, DatabaseConstants.SIZE_OF_INT));
                    currentOffset += DatabaseConstants.SIZE_OF_INT;
                    item = column.Parse(span.Slice(currentOffset, sizeOfValue));
                    values[colIdx] = item;
                    colIdx++;
                    currentOffset += sizeOfValue;
                }
            }
        }

        /// <summary>
        /// Converts a binary array to the body of a local row. The array should include the row size prefix.
        /// </summary>
        /// <param name="span">The bytes to parse. Include the row size prefix in this array.</param>
        /// <param name="sizeOfRow">The total size of the row (parsed from the bytes)</param>
        /// <param name="values">A list of values parse from the array</param>
        /// <param name="schema">The schema of the table</param>
        public static void LocalRowBodyFromBinary(Span<byte> span, out int sizeOfRow, ref RowValue2[] values, ColumnSchema[] schema)
        {
            int colIdx = 0;
            int currentOffset = 0;
            RowValue2 item = null;

            schema.OrderByByteFormat();

            sizeOfRow = BitConverter.ToInt32(span.Slice(0, DatabaseConstants.SIZE_OF_ROW_SIZE));

            currentOffset += DatabaseConstants.SIZE_OF_ROW_SIZE;

            foreach (var column in schema)
            {
                if (!column.IsVariableLength)
                {
                    item = column.Parse(span.Slice(currentOffset, column.Size));
                    values[colIdx] = item;
                    colIdx++;
                    currentOffset += column.Size;
                }
                else
                {
                    int sizeOfValue = DatabaseBinaryConverter.BinaryToInt(span.Slice(currentOffset, DatabaseConstants.SIZE_OF_INT));
                    currentOffset += DatabaseConstants.SIZE_OF_INT;
                    item = column.Parse(span.Slice(currentOffset, sizeOfValue));
                    currentOffset += sizeOfValue;
                }
            }


            /*
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

            throw new NotImplementedException();
        }

        /// <summary>
        /// Copies the rowId and isLocalRow values to the supplied array. This essentially builds a row pre-amble.
        /// </summary>
        /// <param name="array">The array to save the values to</param>
        /// <param name="rowId">The row id</param>
        /// <param name="isLocalRow">True if this is a local row, otherwise false.  </param>
        public static void BuildRowPreamble(ref byte[] array, int rowId, bool isLocalRow)
        {
            byte[] rowIdData = BitConverter.GetBytes(rowId);
            byte[] isLocal = BitConverter.GetBytes(isLocalRow);

            Array.Copy(rowIdData, array, rowIdData.Length);
            Array.Copy(isLocal, 0, array, rowIdData.Length, isLocal.Length);
        }

        public static void BuildRowPreamble(ref RentedByteArray array, int rowId, bool isLocalRow)
        {
            byte[] rowIdData = BitConverter.GetBytes(rowId);
            byte[] isLocal = BitConverter.GetBytes(isLocalRow);

            Array.Copy(rowIdData, array.Array, rowIdData.Length);
            Array.Copy(isLocal, 0, array.Array, rowIdData.Length, isLocal.Length);
        }

        /// <summary>
        /// Saves the list of values provided to the supplied array + the total size of the values. The array must be sized correctly (use ComputeTotalSize() of List of RowValue2 + size of an INT).
        /// This will prefix the array with the total size of the values. This essentially builds a row body (not the pre-amble).
        /// </summary>
        /// <param name="array">The array to save the values to</param>
        /// <param name="values">A list of RowValues to to save to the array</param>
        public static void ValuesToBinaryFormat(ref byte[] array, List<RowValue2> values)
        {
            values.OrderByByteFormat();
            int currentOffset = 0;

            // prefix the array with the total size of all the values

            //int totalRowSize = values.ComputeTotalSize();
            int totalRowSize = values.ComputeTotalSize() + DatabaseConstants.SIZE_OF_ROW_SIZE;

            Debug.WriteLine($"Total Row Size Computed: {totalRowSize.ToString()}");

            byte[] rowSizeArray = BitConverter.GetBytes(totalRowSize);
            Array.Copy(rowSizeArray, 0, array, currentOffset, rowSizeArray.Length);
            currentOffset += rowSizeArray.Length;

            // add the row data
            foreach (var value in values)
            {
                byte[] item = value.GetValueBinaryArrayWithSizePrefix();
                Array.Copy(item, 0, array, currentOffset, item.Length);
                currentOffset += item.Length;
            }
        }

        public static void ValuesToBinaryFormat(ref RentedByteArray array, List<RowValue2> values)
        {
            values.OrderByByteFormat();
            int currentOffset = 0;

            // prefix the array with the total size of all the values

            int totalRowSize = values.ComputeTotalSize();

            Debug.WriteLine($"Total Row Size Computed: {totalRowSize.ToString()}");

            byte[] rowSizeArray = BitConverter.GetBytes(totalRowSize);
            Array.Copy(rowSizeArray, 0, array.Array, currentOffset, rowSizeArray.Length);
            currentOffset += rowSizeArray.Length;

            // add the row data
            foreach (var value in values)
            {
                byte[] item = value.GetValueBinaryArrayWithSizePrefix();
                Array.Copy(item, 0, array.Array, currentOffset, item.Length);
                currentOffset += item.Length;
            }
        }

        /// <summary>
        /// Saves the participantId to the supplied array.
        /// </summary>
        /// <param name="array">The array to save the values to</param>
        /// <param name="participantId">The participant Id</param>
        public static void ParticipantToBinaryFormat(ref byte[] array, Guid? participantId)
        {
            // just save off the participant id (the GUID)
            byte[] item = DatabaseBinaryConverter.GuidToBinary(participantId.Value);
            Array.Copy(item, 0, array, 0, item.Length);
        }

        public static void ParticipantToBinaryFormat(ref RentedByteArray array, Guid? participantId)
        {
            // just save off the participant id (the GUID)
            byte[] item = DatabaseBinaryConverter.GuidToBinary(participantId.Value);
            Array.Copy(item, 0, array.Array, 0, item.Length);
        }

        public static int GetRowIdOffset()
        {
            return 0;
        }

        public static int GetSizeOfRowOffSet()
        {
            return 0;
        }
        #endregion

        #region Private Methods
        private int GetRowSize()
        {
            if (IsLocal)
            {
                return _rowSize;
            }
            else
            {
                return SizeOfParticipantId;
            }
        }

        private void ParseLocalRow()
        {
            // parse data based on column schema
        }

        private void ParseRemoteRow()
        {
            GetParticipantId();
        }


        private int GetSizeOfRow()
        {
            if (IsLocal)
            {
                var span = new ReadOnlySpan<Byte>(_data);
                var bytes = span.Slice(GetSizeOfRowOffSet(), SizeOfRowSize);
                return BitConverter.ToInt32(bytes);
            }
            else
            {
                return DatabaseConstants.PARTICIPANT_ID_SIZE;
            }
        }

        private int GetParticipantOffset()
        {
            return 0;
        }

        private Guid GetParticipantId()
        {
            var span = new ReadOnlySpan<byte>(_data);
            var bytes = span.Slice(GetParticipantOffset(), SizeOfParticipantId);
            return new Guid(bytes);
        }

        private void SetParticipant()
        {
            var data = ParticipantId.ToByteArray();
            data.CopyTo(_data, GetParticipantOffset());
        }

        private void SetSizeOfRow()
        {
            if (IsLocal)
            {
                _rowSize = ComputeRowSize();
                var data = BitConverter.GetBytes(RowSize);
                data.CopyTo(_data, GetSizeOfRowOffSet());
            }
            else
            {
                // we do not need to save the RowSize value bc it is the same as the length of a participant id (a guid)
            }
        }

        private int ComputeRowSize()
        {
            _rowSize = 0;
            // need to compute the size of the row, starting with fixed size columns, then variable columns
            throw new NotImplementedException();
        }
        #endregion
    }

    public class RowPreamble
    {
        #region Private Fields
        private byte[] _data;
        private int _rowId;
        private bool _isLocal;
        #endregion

        #region Public Properties
        public int RowId => _rowId;
        public bool IsLocal => _isLocal;
        public int SizeOfIsLocal => DatabaseConstants.SIZE_OF_IS_LOCAL;
        public int SizeOfRowId => DatabaseConstants.SIZE_OF_ROW_ID;
        public byte[] BinaryData => _data;
        public int Length => _data.Length;
        #endregion

        #region Constructors
        public RowPreamble(byte[] data)
        {
            _data = data;
            ParsePreamble();
        }

        public RowPreamble(Span<byte> data)
        {
            _data = data.ToArray();
            ParsePreamble();
        }

        public RowPreamble(int rowId, bool isLocal)
        {
            _rowId = rowId;
            _isLocal = isLocal;
        }
        #endregion

        #region Public Methods
        public static void Parse(Span<byte> data, out int rowId, out bool isLocal)
        {
            rowId = BitConverter.ToInt32(data);
            isLocal = BitConverter.ToBoolean(data.Slice(DatabaseConstants.SIZE_OF_ROW_ID, DatabaseConstants.SIZE_OF_IS_LOCAL));
        }

        public static void Parse(ReadOnlySpan<byte> data, out int rowId, out bool isLocal)
        {
            rowId = BitConverter.ToInt32(data);
            isLocal = BitConverter.ToBoolean(data.Slice(DatabaseConstants.SIZE_OF_ROW_ID, DatabaseConstants.SIZE_OF_IS_LOCAL));
        }
        #endregion

        #region Private Methods
        private void GetRowId()
        {
            _rowId = BitConverter.ToInt32(_data, 0);
        }

        private void GetIsLocal()
        {
            _isLocal = BitConverter.ToBoolean(new ReadOnlySpan<byte>(_data).Slice(GetIsLocalOffset(), SizeOfIsLocal));
        }

        private void ParsePreamble()
        {
            GetRowId();
            GetIsLocal();
        }

        private int GetIsLocalOffset()
        {
            return SizeOfRowId;
        }
        #endregion

    }
}
