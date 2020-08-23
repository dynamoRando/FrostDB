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
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Table2()
        {
            _btree = new BTreeDictionary<int, Page>();
        }

        public Table2(Process process, TableSchema2 schema) : this()
        {
            _process = process;
            _name = schema.Name;
            _databaseName = schema.DatabaseName;
            _columns = schema.Columns;
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
