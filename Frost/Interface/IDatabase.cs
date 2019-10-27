using FrostDB.Base;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FrostDB.Interface
{
    public interface IDatabase : IFrostObjectGet, IDBObject, ISerializable
    {
        public List<ITable<Column, Row>> Tables { get; }
        public IContract Contract { get; }
        public void AddTable(ITable<Column, Row> table);
        public bool HasTable(string tableName);
        public ITable<Column, Row> GetTable(string tableName);
    }
}
