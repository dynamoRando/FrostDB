using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FrostDB.Interface
{
    public interface IRow : IDBObject, ISerializable
    {
        public Guid? Id { get; }
        public List<IColumn> Columns { get; }
        public List<IRowValue> Values { get; }
    }
}
