using FrostDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IDatabaseFileMapper<TDatabase, YFile, ZManager> 
        where TDatabase : IBaseDatabase 
        where YFile : IDataFile 
        where ZManager : DataManager<TDatabase>
    {
        TDatabase Map(YFile file, ZManager manager);
        YFile Map(TDatabase database);
    }
}
