using FrostDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IDatabaseFileMapper<TDatabase, YFile> 
        where TDatabase : IDatabase 
        where YFile : IDataFile 
    {
        TDatabase Map(YFile file);
        YFile Map(TDatabase database);
    }
}
