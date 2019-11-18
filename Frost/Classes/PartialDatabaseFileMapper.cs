using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class PartialDatabaseFileMapper : IDatabaseFileMapper<PartialDatabase,
        DataFile, DataManager<PartialDatabase>>
    {
        public PartialDatabase Map(DataFile file, DataManager<PartialDatabase> manager)
        {
            throw new NotImplementedException();
        }

        public DataFile Map(PartialDatabase database)
        {
            throw new NotImplementedException();
        }
    }
}
