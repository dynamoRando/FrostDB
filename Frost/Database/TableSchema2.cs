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
        private int _tableId;
        #endregion

        #region Public Properties
        public string Name => _name;
        public string DatabaseName => _databaseName;
        public List<ColumnSchema> Columns => _columns;
        public int TableId => _tableId;
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public TableSchema2(string name, string databaseName) 
        {
            _name = name;
            _databaseName = databaseName;
        }
        public TableSchema2(List<ColumnSchema> columns, string name, string databaseName) : this(name, databaseName)
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
