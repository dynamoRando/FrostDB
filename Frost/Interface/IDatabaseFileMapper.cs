using FrostDB.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IDatabaseFileMapper<T, Y, Z> where T : IDatabase where Y : IDataFile where Z : IDatabaseManager<Database>
    {
        T Map(Y file, Z manager);
    }
}
