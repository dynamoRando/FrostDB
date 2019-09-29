using FrostDB.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IDatabaseManager<T> where T : IDatabase
    {
        List<T> Databases { get; }
        bool HasDatabase(string databaseName);
        bool HasDatabase(Guid guid);
        T GetDatabase(string databaseName);
        T GetDatabase(Guid guid);
        void AddDatabase(Database database);
        void RemoveDatabase(Guid guid);
        void RemoveDatabase(string databaseName);
        int LoadDatabases(string databaseFolderLocation);
        void AddToInbox(IMessage message);
        IDataInboxManager Inbox { get; }
    }
}
