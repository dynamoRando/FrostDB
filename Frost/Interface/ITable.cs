using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface ITable<TColumn, YRow> : IFrostObjectGet, 
        IDBObject where TColumn : IColumn
        where YRow : IRow
    {
        public List<TColumn> Columns { get; }
        public List<YRow> Rows { get; }
        public bool HasRow(YRow row);
        public bool HasRow(Guid guid);
        public YRow GetNewRow();
        public void AddRow(YRow row);
        public void DeleteRow(YRow row);
        public void UpdateRow(YRow row);
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
