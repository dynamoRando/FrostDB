using FrostDB.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IProcess<T, Y> where T : IDatabase, IFrostObjectGet where Y :IDatabase
    {
        List<T> Databases { get; }
        void AddDatabase(string databaseName);
        void RemoveDatabase(Guid guid);
        void RemoveDatabase(string databaseName);
        int LoadDatabases();
        T GetDatabase(string databaseName);
        Y GetPartialDatabase(string databaseName);
    }
}
