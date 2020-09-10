using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class TableSchema2
    {
        #region Private Fields
        private List<ColumnSchema> _columns;
        private string _name;
        private string _databaseName;
        private int _databaseId;
        private int _tableId;
        #endregion

        #region Public Properties
        public string Name => _name;
        public string DatabaseName => _databaseName;
        public List<ColumnSchema> Columns => _columns;
        public int TableId => _tableId;
        public int DatabaseId => _databaseId;
        public BTreeAddress BTreeAddress => new BTreeAddress { DatabaseId = _databaseId, TableId = _tableId };
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public TableSchema2(int id, string name, string databaseName, int databaseId) 
        {
            _name = name;
            _databaseName = databaseName;
            _tableId = id;
            _databaseId = databaseId;
        }
        public TableSchema2(List<ColumnSchema> columns, int id, string name, string databaseName, int databaseId) : this(id, name, databaseName, databaseId)
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
