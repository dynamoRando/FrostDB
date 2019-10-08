using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base.Extensions
{
    public static class Extensions
    {
        public static Row Fetch(this RowReference reference)
        {
            Guid id = reference.Id;
            /*
             * write whatever logic needed here to connect to the remote location
             * for this row
             * and get the row based on the reference.Id
             */

            throw new NotImplementedException();
        }
    }
}
