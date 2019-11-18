﻿using FrostDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IBaseDatabase
    {
        Guid? Id { get; }
        Table GetTable(Guid? tableId);
        string Name { get; }
        List<Table> Tables { get; }
        Table GetTable(string tableName);
        bool HasTable(string tableName);
        void AddTable(Table table);
        void UpdateSchema();
        DbSchema Schema { get; }
        List<Participant> Participants { get; }
        Contract Contract { get;  }
    }
}
