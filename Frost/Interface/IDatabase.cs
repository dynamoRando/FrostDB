using FrostDB.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IDatabase : IFrostObjectGet, IDBObject
    {
        public List<ITable<Column, Row>> Tables { get; }
        public IContract Contract { get; }
        public void AddTable(ITable<Column, Row> table);
    }
}
