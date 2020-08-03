using FrostDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface ITable
    {
        Row GetRow(RowReference reference);
        bool HasRow(Guid? rowId);
        Row GetRow(Guid? rowId);
        bool AddColumn(string columnName, Type type);
        void RemoveColumn(string columnName);
    }
}
