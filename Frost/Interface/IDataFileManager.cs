using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IDataFileManager<T>
        where T : IDataFile
    {
        T GetDataFile(string fileLocation);
        void SaveDataFile(string fileLocation, T dataFile);
    }
}
