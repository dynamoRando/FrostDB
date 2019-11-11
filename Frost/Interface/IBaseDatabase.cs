using FrostDB.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IBaseDatabase
    {
        Guid? Id { get; }
        BaseTable GetTable(Guid? tableId);
        string Name { get; }
        List<BaseTable> Tables { get; }
        BaseTable GetTable(string tableName);
        bool HasTable(string tableName);
        void AddTable(BaseTable table);
        void UpdateSchema();
        DbSchema Schema { get; }
    }
}
