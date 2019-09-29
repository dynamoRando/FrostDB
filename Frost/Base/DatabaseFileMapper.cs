using System;
using System.Collections.Generic;
using System.Text;
using FrostDB.Interface;

namespace FrostDB.Base
{
    public class DatabaseFileMapper : IDatabaseFileMapper<Database, IDataFile, DatabaseManager>
    {
        public Database Map(IDataFile file, DatabaseManager manager)
        {
            var database = new Database(file.Name, manager, file.Id.Value);
            
            // TODO Need to map tables, etc.

            return database;
        }
    }
}
