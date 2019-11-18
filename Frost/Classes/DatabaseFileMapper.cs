using System;
using System.Collections.Generic;
using System.Text;
using FrostDB.Interface;

namespace FrostDB
{
    public class DatabaseFileMapper : IDatabaseFileMapper<IBaseDatabase, DataFile, DataManager<IBaseDatabase>>
    {
        public Database MapDatabase(DataFile file, DataManager<IBaseDatabase> manager)
        {
            var database = new Database(
                file.Name,
                manager,
                file.Id.Value,
                file.Tables,
                file.Schema,
                file.Participants,
                file.Contract
                ); 
      
            return database;
        }

        public DataFile Map(IBaseDatabase database)
        {
            // TODO need to map tables, etc
            return new DataFile { 
                Id = database.Id, 
                Name = database.Name, 
                Tables = database.Tables, 
                Schema = database.Schema,
                Participants = database.Participants,
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

        public IBaseDatabase Map(DataFile file, DataManager<IBaseDatabase> manager)
        {
            var database = new Database(
                file.Name,
                manager,
                file.Id.Value,
                file.Tables,
                file.Schema,
                file.Participants,
                file.Contract
                );

            return database;
        }
    }
}
