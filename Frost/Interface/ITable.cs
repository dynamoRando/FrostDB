using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FrostDB.Interface
{
    public interface ITable<TColumn, YRow> : IFrostObjectGet, 
        IDBObject where TColumn : IColumn
        where YRow : IRow,
        ISerializable
    {
        List<TColumn> Columns { get; }
        List<YRow> Rows { get; }
        bool HasRow(YRow row);
        bool HasRow(Guid guid);
        YRow GetNewRow();
        void AddRow(YRow row);
        void DeleteRow(YRow row);
        void UpdateRow(YRow oldRow, YRow newRow);
        List<YRow> GetRows(string queryString);
        Guid? DatabaseId { get; set; }
        TColumn GetColumn(Guid? id);
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
