using FrostDB.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IBaseDatabase
    {
        Guid? Id { get; set; }
        IBaseTable GetTable(Guid? tableId);
    }
}
