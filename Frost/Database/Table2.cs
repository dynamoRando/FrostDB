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
        public bool HasIndexes { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Table2(Process process, string databaseName)
        {
            _process = process;
            _databaseName = databaseName;
            _btree = new BTreeDictionary<int, Page>();
            _tableId = _process.GetDatabase2(_databaseName).GetNextTableId();
        }

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
        #endregion

        #region Private Methods
        #endregion

    }
}
