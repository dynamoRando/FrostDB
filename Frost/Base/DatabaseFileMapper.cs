﻿using System;
using System.Collections.Generic;
using System.Text;
using FrostDB.Interface;

namespace FrostDB.Base
{
    public class DatabaseFileMapper : IDatabaseFileMapper<Database, DataFile, DatabaseManager>
    {
        public Database Map(DataFile file, DatabaseManager manager)
        {
            var database = new Database(file.Name, manager, file.Id.Value);

            // TODO Need to map tables, etc.

            return database;
        }

        public DataFile Map(Database database)
        {
            return new DataFile { Id = database.Id, Name = database.Name };
        }
    }
}
