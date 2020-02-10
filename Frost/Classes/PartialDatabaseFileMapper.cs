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
            return new DataFile
            {
                Id = database.Id,
                Name = database.Name,
                Tables = database.Tables,
                Schema = database.Schema,
                Contract = database.Contract
            };
        }

    }
}
