using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base.Extensions
{
    public static class Extensions
    {
        public static Row GetData(this RowReference reference)
        {
            Guid? id = reference.Id;
            /*
             * write whatever logic needed here to connect to the remote location
             * for this row
             * and get the row based on the reference.Id
             * will likely need a reference to the comm service to get data in
             * an async manner
             */

            throw new NotImplementedException();
        }
    }
}
