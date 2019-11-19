using FrostDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IDatabase
    {
        Guid? Id { get; }
        Table GetTable(Guid? tableId);
        string Name { get; }
        List<Table> Tables { get; }
        Table GetTable(string tableName);
        bool HasTable(string tableName);
        bool HasTable(Guid? tableName);
        void AddTable(Table table);
        void UpdateSchema();
        DbSchema Schema { get; }
        List<Participant> AcceptedParticipants { get; }
        List<Participant> PendingParticipants { get; }
        Contract Contract { get; }
        void AddParticipant(Participant participant);
    }
}
