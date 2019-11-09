using FrostDB.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IBaseTable
    {
        Row GetRow(BaseRowReference reference);
    }
}
