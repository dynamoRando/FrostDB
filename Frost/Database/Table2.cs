﻿using MoreLinq;
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
        private List<ColumnSchema> _columns;
        private string _name;
        private string _databaseName;
        private int _tableId;
        #endregion

        #region Public Properties
        public List<ColumnSchema> Columns => _columns;
        public string Name => _name;
        public string Database => _databaseName;
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
        /// Constructs a brand new table with the specified table name. Used when adding a new table to a database.
        /// </summary>
        /// <param name="process">The FrostDB Process.</param>
        /// <param name="databaseName">The database that this table is attached to.</param>
        public Table2(Process process, string databaseName)
        {
            _process = process;
            _databaseName = databaseName;
            _btree = new BTreeDictionary<int, Page>();
            _tableId = _process.GetDatabase2(_databaseName).GetNextTableId();
        }

        /// <summary>
        /// Constructs a table based on the specified TableSchema. Used when loading FrostDB from disk.
        /// </summary>
        /// <param name="process">The FrostDB process.</param>
        /// <param name="schema">The table schema (loaded from disk.)</param>
        public Table2(Process process, TableSchema2 schema)
        {
            _btree = new BTreeDictionary<int, Page>();
            _process = process;
            _name = schema.Name;
            _databaseName = schema.DatabaseName;
            _columns = schema.Columns;
            _tableId = schema.TableId;
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
        #endregion

    }
}
