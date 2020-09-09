using C5;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace FrostDB
{
    public class Table2
    {
        #region Private Fields
        private Process _process;
        private int _maxPageId;
        private TableSchema2 _schema;
        private List<ColumnSchema> _columns;
        private string _name;
        private string _databaseName;
        private int _tableId;
        private int _databaseId;

        // this is an in memory conversion from the pages and should be destructive
        // i.e. always destroyed and rebuilt from the pager
        private List<Row2> _rows;
        #endregion

        #region Public Properties
        public TableSchema2 Schema => _schema;
        public List<ColumnSchema> Columns => _columns;
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
        public Table2(Process process, TableSchema2 schema)
        {
            _process = process;
            _schema = schema;
            _name = schema.Name;
            _databaseName = schema.DatabaseName;
            _columns = schema.Columns;
            _tableId = schema.TableId;
            _databaseId = schema.DatabaseId;
            _rows = new List<Row2>();
            // need to figure out what the new b-tree structure will look like
            // how to populate binary page data from disk to table object?
            throw new NotImplementedException();
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
        public List<Row2> GetAllRows()
        {
            return _process.GetDatabase2(_databaseId).Storage.GetAllRows(new BTreeAddress { DatabaseId = _databaseId, TableId = _tableId });
        }

        /// <summary>
        /// Returns an empty row form to be filled out with values to add back to the table.
        /// </summary>
        /// <returns>A row form.</returns>
        public RowForm2 GetNewRow()
        {
            var result = new RowForm2();
            result.DatabaseName = DatabaseName;
            result.TableName = Name;

            foreach (var c in Columns)
            {
                result.Values.Add(new RowValue2 { Column = c });
            }

            return result;
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

            if (rowForm.IsLocal)
            {
                isSuccessful = AddRowLocally(rowForm);
            }
            else
            {
                isSuccessful = AddRowRemotely(rowForm);
            }

            return isSuccessful;
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
            RowInsert rowToInsert = new RowInsert { Table = this.Schema, Values = rowForm.Values };

            GetDatabase().Storage.WriteTransactionForInsert(rowToInsert);

            throw new NotImplementedException();
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
        #endregion

    }
}
