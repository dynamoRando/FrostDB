using FrostDB.Base;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FrostDB.Interface
{
    public interface IRow : IDBObject, ISerializable
    {
        public Guid? Id { get; }
        public List<Column> Columns { get; }
        public List<RowValue> Values { get; }
    }
}
