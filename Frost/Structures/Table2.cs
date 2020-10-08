using C5;
using FrostCommon;
using FrostCommon.DataMessages;
using Google.Protobuf.Reflection;
using MoreLinq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrostDB
{
    /// <summary>
    /// A database structure representing a SQL table. This structure does not actually hold any data, but instead coordinates actions
    /// between cache (b-trees), storage (disk) and if applicable, network.
    /// </summary>
    public class Table2
    {
        #region Private Fields
        private Process _process;
        private int _maxPageId;
        private TableSchema2 _schema;
        private ColumnSchema[] _columns;
        private string _name;
        private string _databaseName;
        private int _tableId;
        private int _databaseId;
        private DbStorage _storage;
        private Cache _cache;
        private BTreeAddress _address;
        #endregion

        #region Public Properties
        public TableSchema2 Schema => _schema;
        public ColumnSchema[] Columns => _columns;
        public string Name => _name;
        public string DatabaseName => _databaseName;
        public int TableId => _tableId;
        // change this - placeholder idea to check to see if indexes exist on table to search for later
        // ideally we'd check the database file to see if an index exists to return true/false
        // if we have an index we can "jump" to the PageId of the corresponding value
        public bool HasIndexes { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs a table based on the specified TableSchema. Used when loading FrostDB from disk.
        /// </summary>
        /// <param name="process">The FrostDB process.</param>
        /// <param name="schema">The table schema (loaded from disk, usually from a DBFill object.)</param>
        /// <param name="dbStorage">The backing storage object for the database this table is attached to</param>
        /// <param name="pager">The in memory cache for FrostDb</param>
        public Table2(Process process, TableSchema2 schema, DbStorage dbStorage, Cache pager)
        {
            _process = process;
            _schema = schema;
            _name = schema.Name;
            _databaseName = schema.DatabaseName;
            _columns = schema.Columns;
            _tableId = schema.TableId;
            _databaseId = schema.DatabaseId;
            _storage = dbStorage;
            _cache = pager;
            _address = new BTreeAddress { DatabaseId = _databaseId, TableId = _tableId };
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Returns rows that match the conditions specified in the parameters.
        /// </summary>
        /// <param name="columnName">The column name to be searched.</param>
        /// <param name="operation">The comparison operator (i.e. symbol for greater than, less than, etc.)</param>
        /// <param name="value">The value you are searching for.</param>
        /// <returns>A list of rows that match the given condition.</returns>
        /// <remarks>This functionality is similar to what is in SearchStep.cs. I hoped to only search for a single condition
        /// rather than passing a list of multiple conditions and trying to AND/OR them together.</remarks>
        public List<Row2> GetRowsWithValue(string columnName, string operation, string value)
        {
            var result = new List<Row2>();

            if (HasIndexes)
            {
                // refer to the index so that we can jump to the correct b-tree page
            }
            else
            {

            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns all rows of the table.
        /// </summary>
        /// <returns>A list of rows in the table.</returns>
        public List<RowStruct> GetAllRows()
        {
            throw new NotImplementedException();

            var rows = _cache.GetAllRowsSpan(_address);
            List<RowStruct> remoteRows = new List<RowStruct>();

            foreach(var row in rows)
            {
                if (!row.IsLocal)
                {
                    // need to get the row from the participant (network call)
                    var remoteRow = GetRemoteRow(row.ParticipantId);
                    remoteRows.Add(remoteRow);
                }
            }
        }

        public ColumnSchema GetColumn(int ordinal)
        {
            return Columns.Where(column => column.Ordinal == ordinal).FirstOrDefault();
        }

        /// <summary>
        /// Returns an empty row form to be filled out with values to add back to the table.
        /// </summary>
        /// <returns>A row form.</returns>
        public RowForm2 GetNewRow()
        {
            return new RowForm2(DatabaseName, Name, Columns, _address);
        }

        /// <summary>
        /// Adds a row to the table based on the information in the passed in form.
        /// </summary>
        /// <param name="rowForm">A row form.</param>
        /// <returns></returns>
        public bool AddRow(RowForm2 rowForm)
        {
            bool isSuccessful;

            if (rowForm is null)
            {
                throw new ArgumentNullException(nameof(rowForm));
            }

            if (rowForm.IsLocal(_process))
            {
                isSuccessful = AddRowLocally(rowForm);
            }
            else
            {
                isSuccessful = AddRowRemotely(rowForm);
            }

            return isSuccessful;
        }

        /// <summary>
        /// Returns true if the table has the specified columnName, otherwise false
        /// </summary>
        /// <param name="columnName">The column name to check for</param>
        /// <returns>True if the table has it, otherwise false</returns>
        public bool HasColumn(string columnName)
        {
            return _schema.Columns.Any(col => col.Name.ToUpper() == columnName.ToUpper());
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Adds a row to this FrostDb instance
        /// </summary>
        /// <param name="rowForm">The row data to add</param>
        /// <returns>True if successful, otherwise false</returns>
        private bool AddRowLocally(RowForm2 rowForm)
        {
            RowInsert rowToInsert = new RowInsert(rowForm.Values, this.Schema, rowForm.Participant.Id, !rowForm.IsLocal(_process), rowForm.Address);

            if (_storage.RecordTransactionInLog(rowToInsert))
            {
                if (_cache.InsertRow(rowToInsert))
                {
                    if (_storage.UpdateIndexes(rowToInsert))
                    {
                        _cache.SyncTreeToDisk(rowToInsert.Address);
                        _storage.MarkTransactionAsReconciledInLog(rowToInsert);
                    }
                }
            }

            //throw new NotImplementedException();
            return true;
        }

        /// <summary>
        /// Adds a row to a remote FrostDb instance
        /// </summary>
        /// <param name="rowForm">The row data to add</param>
        /// <returns>True if successful, otherwise false</returns>
        private bool AddRowRemotely(RowForm2 rowForm)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the database that this table belongs to
        /// </summary>
        /// <returns>The database this table belongs to</returns>
        private Database2 GetDatabase()
        {
            return _process.GetDatabase2(_databaseId);
        }

        /// <summary>
        /// Gets the row from the specified participant
        /// </summary>
        /// <param name="participantId">The participant id</param>
        /// <returns>The row from the participant</returns>
        private RowStruct GetRemoteRow(Guid participantId)
        {
            throw new NotImplementedException();
            //Row row = new Row();
            //RemoteRowInfo request = BuildRemoteRowInfo();
            //string content = JsonConvert.SerializeObject(request);
            //Guid? requestId = Guid.NewGuid();
            //Message rowMessage = null;

            //var getRowMessage = _process.Network.BuildMessage(Participant.Location, content, MessageDataAction.Process.Get_Remote_Row, MessageType.Data, requestId, MessageActionType.Table, request.GetType());
            //rowMessage = _process.Network.SendMessage(getRowMessage);

            //if (rowMessage != null)
            //{
            //    if (rowMessage.Content != null)
            //    {
            //        row = rowMessage.GetContentAs<Row>();
            //    }
            //}
        }
        #endregion

    }
}
