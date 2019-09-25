using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface ITable : IFrostObjectGet
    {
        public List<IColumn> Columns { get; }
        public List<IRow> Rows { get; }
        public bool HasRow(IRow row);
        public bool HasRow(Guid guid);
        public IRow GetNewRow();
        public void AddRow(IRow row);
        public void DeleteRow(IRow row);
        public void UpdateRow(IRow row);
    }
}
