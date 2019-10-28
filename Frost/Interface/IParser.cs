using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IParser
    {
        static List<IRowValue> GetValues(string condition);
    }
}
