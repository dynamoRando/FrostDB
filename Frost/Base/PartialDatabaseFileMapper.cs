using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
{
    public class PartialDatabaseFileMapper : IDatabaseFileMapper<BasePartialDatabase,
        DataFile, BaseDataManager<BasePartialDatabase>>
    {
        public BasePartialDatabase Map(DataFile file, BaseDataManager<BasePartialDatabase> manager)
        {
            throw new NotImplementedException();
        }

        public DataFile Map(BasePartialDatabase database)
        {
            throw new NotImplementedException();
        }
    }
}
