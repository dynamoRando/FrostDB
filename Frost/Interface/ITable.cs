using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface ITable<TColumn> : IFrostObjectGet, IDBObject where TColumn : IColumn 
    {
        public List<TColumn> Columns { get; }
        public List<IRow> Rows { get; }
        public bool HasRow(IRow row);
        public bool HasRow(Guid guid);
        public IRow GetNewRow();
        public void AddRow(IRow row);
        public void DeleteRow(IRow row);
        public void UpdateRow(IRow row);
    }
}
/*
 * public interface ITable<TColumn, TRow> : IFrostObjectGet, IDBObject where TColumn : IColumn where TRow : IRow 
    {
        public List<TColumn> Columns { get; }
        public List<TRow> Rows { get; }
        public bool HasRow(TRow row);
        public bool HasRow(Guid guid);
        public TRow GetNewRow();
        public void AddRow(TRow row);
        public void DeleteRow(TRow row);
        public void UpdateRow(TRow row);
    }
 */
