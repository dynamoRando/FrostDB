using System;
using System.Collections.Generic;
using System.Text;
using FrostDB.Interface;

namespace FrostDB
{
    public class DatabaseFileMapper : IDatabaseFileMapper<Database, DataFile>
    {
        public DataFile Map(Database database)
        {
            // TODO need to map tables, etc
            return new DataFile { 
                Id = database.Id, 
                Name = database.Name, 
                Tables = database.Tables, 
                Schema = database.Schema,
                AcceptedParticipants = database.AcceptedParticipants,
                PendingParticipants = database.PendingParticipants,
                Contract = database.Contract
            };
            /*
             * foreach table in Itables
             * switch (table.type)
             * case when type == regular, add to regular tables
             * case when type == coop, add to coop tables
             */
        }

        public Database Map(DataFile file, Process process)
        {
            var database = new Database(
              file.Name,
              file.Id.Value,
              file.Tables,
              file.Schema,
              file.AcceptedParticipants,
              file.PendingParticipants,
              file.Contract,
              process
              );

            return database;
        }
    }
}
