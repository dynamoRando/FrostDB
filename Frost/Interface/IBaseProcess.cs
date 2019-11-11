﻿using FrostDB.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IBaseProcess<T> where T : IBaseDatabase 
    {
        List<T> Databases { get; }
        void AddDatabase(string databaseName);
        void RemoveDatabase(Guid guid);
        void RemoveDatabase(string databaseName);
        int LoadDatabases();
        T GetDatabase(string databaseName);
    }
}