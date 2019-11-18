using FrostDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IBaseTable
    {
        Row GetRow(RowReference reference);
    }
}
