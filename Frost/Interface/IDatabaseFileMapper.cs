using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IDatabaseFileMapper<T, Y, Z> where T : IDatabase where Y : IDataFile where Z : IDatabaseManager
    {
        T Map(Y file, Z manager);
    }
}
