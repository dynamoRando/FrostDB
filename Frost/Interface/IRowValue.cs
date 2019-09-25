using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IRowValue
    {
        public IColumn Column { get; }
        public object Value { get; set; }
    }
}
