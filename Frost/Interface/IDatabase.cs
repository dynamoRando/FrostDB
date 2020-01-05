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
        string GetTableName(Guid? tableId);
        bool HasTable(string tableName);
        bool HasTable(Guid? tableName);
        void AddTable(Table table);
        void RemoveTable(string tableName);
        void RemoveTable(Guid? tableId);
        void UpdateSchema();
        DbSchema Schema { get; }
        List<Participant> AcceptedParticipants { get; }
        List<Participant> PendingParticipants { get; }
        Contract Contract { get; }
        void AddParticipant(Participant participant);
        void AddPendingParticipant(Participant participant);
        bool IsCooperative();
        bool HasParticipant(Guid? participantId);
        Participant GetParticipant(Guid? participantId);
    }
}
