using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
{
    public class Row : IRow
    {
        public Guid Id => throw new NotImplementedException();
        public List<IColumn> Columns => throw new NotImplementedException();
        public List<IRowValue> Values => throw new NotImplementedException();
    }
}
