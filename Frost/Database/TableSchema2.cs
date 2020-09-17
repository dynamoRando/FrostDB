using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class TableSchema2
    {
        #region Private Fields
        private ColumnSchema[] _columns;
        private string _name;
        private string _databaseName;
        private int _databaseId;
        private int _tableId;
        #endregion

        #region Public Properties
        public string Name => _name;
        public string DatabaseName => _databaseName;
        public ColumnSchema[] Columns => _columns;
        public int TableId => _tableId;
        public int DatabaseId => _databaseId;
        public BTreeAddress BTreeAddress => new BTreeAddress { DatabaseId = _databaseId, TableId = _tableId };
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public TableSchema2(int id, string name, string databaseName, int databaseId, int numOfColumns) 
        {
            _name = name;
            _databaseName = databaseName;
            _tableId = id;
            _databaseId = databaseId;
            _columns = new ColumnSchema[numOfColumns];
        }
        public TableSchema2(ColumnSchema[] columns, int id, string name, string databaseName, int databaseId) : this(id, name, databaseName, databaseId, columns.Length)
        {
            _columns = columns;
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

    }
}
