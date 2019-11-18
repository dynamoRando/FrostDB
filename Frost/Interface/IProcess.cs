using FrostDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IProcess<T> where T : IBaseDatabase, IFrostObjectGet
    {
        List<T> Databases { get; }
        void AddDatabase(string databaseName);
        void AddPartialDatabase(string databaseName);
        void RemoveDatabase(Guid guid);
        void RemoveDatabase(string databaseName);
        int LoadDatabases();
        T GetDatabase(string databaseName);
    }
}
