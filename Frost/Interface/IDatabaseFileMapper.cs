using FrostDB.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IDatabaseFileMapper<TDatabase, YFile, ZManager> 
        where TDatabase : IDatabase 
        where YFile : IDataFile 
        where ZManager : DataManager<TDatabase>
    {
        TDatabase Map(YFile file, ZManager manager);
        YFile Map(TDatabase database);
    }
}
