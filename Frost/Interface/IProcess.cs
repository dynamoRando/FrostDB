﻿using FrostDB.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IProcess<T> where T : IDatabase, IFrostObjectGet
    {
        List<T> Databases { get; }
        void AddDatabase(string databaseName);
        void RemoveDatabase(Guid guid);
        void RemoveDatabase(string databaseName);
        int LoadDatabases();
    }
}
