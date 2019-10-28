using FrostDB.Enum;
using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
{
    public class RowValueQueryParam : IRowValueQueryParam
    {
        #region Public Properties
        public IColumn Column { get; set; }
        public RowValueQuery QueryType { get; set; }
        public List<object> Values { get; set; }
        public object MinValue { get; set; }
        public object MaxValue { get; set; }
        #endregion

        #region Constructors
        public RowValueQueryParam() { }
        public RowValueQueryParam(IColumn column)
        {
            Column = column;
        }
        #endregion
    }
}
