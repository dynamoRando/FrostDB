using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class PartialDatabaseFileMapper : IDatabaseFileMapper<IDatabase, DataFile, DataManager<IDatabase>>
    {
        public IDatabase Map(DataFile file, DataManager<IDatabase> manager)
        {
            var database = new PartialDatabase(
             file.Name,
             manager,
             file.Id.Value,
             file.Tables
             );

            return database;
        }

        public DataFile Map(IDatabase database)
        {
            throw new NotImplementedException();
        }
    }
}
