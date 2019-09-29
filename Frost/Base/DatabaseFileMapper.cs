using System;
using System.Collections.Generic;
using System.Text;
using FrostDB.Interface;

namespace FrostDB.Base
{
    public class DatabaseFileMapper : IDatabaseFileMapper<IDatabase, IDataFile, DatabaseManager>
    {
        public IDatabase Map(IDataFile file, DatabaseManager manager)
        {
            var database = new Database(file.Name, manager, file.Id.Value);
            
            return database;
        }
    }
}
