using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IDatabase : IFrostObjectGet
    {
        public List<ITable> Tables { get; }
        public void AddTable(ITable table);
    }
}
