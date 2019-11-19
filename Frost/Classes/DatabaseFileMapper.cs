using System;
using System.Collections.Generic;
using System.Text;
using FrostDB.Interface;

namespace FrostDB
{
    public class DatabaseFileMapper : IDatabaseFileMapper<IDatabase, DataFile, DataManager<IDatabase>>
    {
        public Database MapDatabase(DataFile file, DataManager<IDatabase> manager)
        {
            var database = new Database(
                file.Name,
                manager,
                file.Id.Value,
                file.Tables,
                file.Schema,
                file.AcceptedParticipants,
                file.PendingParticipants,
                file.Contract
                ); 
      
            return database;
        }

        public DataFile Map(IDatabase database)
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

        public DataFile Map(Database database)
        {
            throw new NotImplementedException();
        }

        public IDatabase Map(DataFile file, DataManager<IDatabase> manager)
        {
            var database = new Database(
                file.Name,
                manager,
                file.Id.Value,
                file.Tables,
                file.Schema,
                file.AcceptedParticipants,
                file.PendingParticipants,
                file.Contract
                );

            return database;
        }
    }
}
