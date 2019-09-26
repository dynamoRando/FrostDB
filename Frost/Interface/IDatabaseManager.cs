using FrostDB.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IDatabaseManager
    {
        List<IDatabase> Databases { get; }
        bool HasDatabase(string databaseName);
        bool HasDatabase(Guid guid);
        IDatabase GetDatabase(string databaseName);
        IDatabase GetDatabase(Guid guid);
        void AddDatabase(Database database);
        void RemoveDatabase(Guid guid);
        void RemoveDatabase(string databaseName);
        int LoadDatabases(string databaseFolderLocation);
        void AddToInbox(IMessage message);
    }
}
