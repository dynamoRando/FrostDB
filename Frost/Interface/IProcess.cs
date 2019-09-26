using FrostDB.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IProcess : IFrostObjectGet
    {
        List<IDatabase> Databases { get; }
        void AddDatabase(Database database);
        void RemoveDatabase(Guid guid);
        void RemoveDatabase(string databaseName);
        int LoadDatabases();
    }
}
