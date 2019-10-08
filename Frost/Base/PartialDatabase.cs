using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
{
    public class PartialDatabase : IDatabase
    {
        public List<ITable<Column>> Tables => throw new NotImplementedException();

        public IContract Contract => throw new NotImplementedException();

        public Guid? Id => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        public void AddTable(ITable<Column> table)
        {
            throw new NotImplementedException();
        }
    }
}
