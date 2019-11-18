using FrostDB;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FrostDB.Interface
{
    public interface IRow : IDBObject, ISerializable
    {
        public Guid? Id { get; }
        public Guid? TableId { get; }
        public List<Guid?> ColumnIds { get; }
        public List<RowValue> Values { get; }
    }
}
