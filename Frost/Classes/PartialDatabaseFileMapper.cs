using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class PartialDatabaseFileMapper : IDatabaseFileMapper<PartialDatabase, DataFile>
    {
        public PartialDatabase Map(DataFile file)
        {
            var database = new PartialDatabase(
            file.Name,
            file.Id.Value,
            file.Tables
            );

            return database;
        }

        public DataFile Map(PartialDatabase database)
        {
            throw new NotImplementedException();
        }

    }
}
