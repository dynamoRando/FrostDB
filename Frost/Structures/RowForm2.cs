using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FrostDB
{
    /// <summary>
    /// Used to pass row values to be added to a table.
    /// </summary>
    public class RowForm2
    {
        #region Private Fields
        private string _tableName;
        private string _databaseName;
        private ColumnSchema[] _columns;
        private List<RowValue2> _values;
        private BTreeAddress _address;
        #endregion

        #region Public Properties
        public string DatabaseName => _databaseName;
        public string TableName => _tableName;
        public List<RowValue2> Values => _values;
        public Participant2 Participant { get; set; }
        public ColumnSchema[] Columns => _columns;
        public BTreeAddress Address => _address;
        #endregion

        #region Constructors
        public RowForm2(string databaseName, string tableName, ColumnSchema[] columns, int databaseId, int tableId)
        {
            _columns = columns;
            _databaseName = databaseName;
            _tableName = tableName;
            _values = new List<RowValue2>();
            SetColumnsForValues();
            _address = new BTreeAddress { DatabaseId = databaseId, TableId = tableId };
        }

        public RowForm2(string databaseName, string tableName, ColumnSchema[] columns, BTreeAddress address) 
            : this(databaseName, tableName, columns, address.DatabaseId, address.TableId)
        {
        }
        #endregion

        #region Public Methods
        public void SetValue(string columnName, string value)
        {
            var item = _values.Where(value => value.Column.Name == columnName).FirstOrDefault();

            if (item == null)
            {
                throw new ArgumentException($"the column {columnName} was not found");
            }

            item.Value = value;
        }
        #endregion

        #region Private Methods
        private void SetColumnsForValues()
        {
            foreach (var column in _columns)
            {
                _values.Add(new RowValue2 { Column = column });
            }
        }
        #endregion

    }
}
