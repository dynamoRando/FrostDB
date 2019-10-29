using FrostDB.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IRowValue
    {
        public Guid? ColumnId { get; }
        public string ColumnName { get; set; }
        public object Value { get; set; }
    }
}
