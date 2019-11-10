using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace FrostDB.Interface
{
    public interface IDataFileManager<T>
        where T : IDataFile
    {
        T GetDataFile(string fileLocation);
        void SaveDataFile(string fileLocation, T dataFile);
        ReaderWriterLockSlim State { get; }
    }
}
