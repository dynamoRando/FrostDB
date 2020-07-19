using FrostDB.Enum;
using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class RowValueQueryParam : IRowValueQueryParam
    {
        #region Public Properties
        public string ColumnName { get; set; }
        public Type ColumnDataType { get; set; }
        public RowValueQuery QueryType { get; set; }
        public object Value { get; set; }
        public object MinValue { get; set; }
        public object MaxValue { get; set; }
        #endregion

        #region Constructors
        public RowValueQueryParam() { }
        public RowValueQueryParam(string columnName, Type columnDataType)
        {
            ColumnName = columnName;
            ColumnDataType = columnDataType;
        }
        #endregion
    }
}
