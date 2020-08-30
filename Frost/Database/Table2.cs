using MoreLinq;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class Table2
    {
        #region Private Fields
        private Process _process;
        private BTreeDictionary<int, Page> _btree;
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
            _btree = new BTreeDictionary<int, Page>();
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
                // we're going to scan every page in the b-tree

                // populate the tree with the 1st page from pager
                CheckAndPrimeTree();

                // convert the pages in the tree to rows
                result.AddRange(GetRowsFromTree());
                

                // need to scan the list of rows to see if any of the values match the parameters specified

                // if not, we need to go back to the pager and get more pages until we've pulled all pages 
                // all pages meaning keep going to cache, then disk, until we've got all pages in memory or 
                // until we satisfy our search condition

                // logic flaw - what if all the rows meet our search condition? either way
                // we must traverse all pages for the table to ensure we have all rows

                throw new NotImplementedException();
               
            }

            return result;
        }

        /// <summary>
        /// Returns all rows of the table.
        /// </summary>
        /// <returns>A list of rows in the table.</returns>
        public List<Row2> GetAllRows()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Converts all the pages in the tree to row items
        /// </summary>
        /// <returns>A list of rows</returns>
        private List<Row2> GetRowsFromTree()
        {
            _rows.Clear();

            // from the tree, convert to a list of rows
            _btree.ForEach(item =>
            {
                _rows.AddRange(item.Value.GetValues(_schema));
            });

            return _rows;
        }

        /// <summary>
        /// If the tree is empty, pull the first page out from the pager
        /// </summary>
        private void CheckAndPrimeTree()
        {
            if (_btree.Count == 0)
            {
                var page = _process.GetDatabase2(_databaseId).Storage.GetAPage();
                _btree.Add(new KeyValuePair<int, Page>(page.Address.PageId, page));
            }
        }
        #endregion

    }
}
