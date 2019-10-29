using FrostDB.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IRowValueQueryParam
    {
        string ColumnName { get; set; }
        Type ColumnDataType { get; set; }
        RowValueQuery QueryType { get; set; }
        Object Value { get; set; }
    }
}
