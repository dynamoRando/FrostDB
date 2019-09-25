using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IRow
    {
        public Guid Id { get; }
        public List<IColumn> Columns { get; }
        public List<IRowValue> Values { get; }
    }
}
