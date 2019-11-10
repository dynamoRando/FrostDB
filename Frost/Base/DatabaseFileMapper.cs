using System;
using System.Collections.Generic;
using System.Text;
using FrostDB.Interface;

namespace FrostDB.Base
{
    public class DatabaseFileMapper : IDatabaseFileMapper<IBaseDatabase, DataFile, BaseDataManager<IBaseDatabase>>
    {
        public BaseDatabase Map(DataFile file, BaseDataManager<IBaseDatabase> manager)
        {
            var database = new BaseDatabase(
                file.Name, 
                manager, 
                file.Id.Value,
                file.Tables
                );
      
            // TODO Need to map tables, etc.

            return database;
        }

        public DataFile Map(IBaseDatabase database)
        {
            // TODO need to map tables, etc
            return new DataFile { Id = database.Id, Name = database.Name, Tables = database.Tables };
            /*
             * foreach table in Itables
             * switch (table.type)
             * case when type == regular, add to regular tables
             * case when type == coop, add to coop tables
             */
        }

        public DataFile Map(BaseDatabase database)
        {
            throw new NotImplementedException();
        }

        IBaseDatabase IDatabaseFileMapper<IBaseDatabase, DataFile, BaseDataManager<IBaseDatabase>>.Map(DataFile file, BaseDataManager<IBaseDatabase> manager)
        {
            throw new NotImplementedException();
        }
    }
}
