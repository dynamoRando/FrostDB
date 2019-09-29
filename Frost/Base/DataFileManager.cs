using Newtonsoft.Json;
using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FrostDB.Base
{
    public class DataFileManager : IDataFileManager<DataFile>
    {
        public DataFile GetDataFile(string fileLocation)
        {
            var dbJson = File.ReadAllText(fileLocation);
            return JsonConvert.DeserializeObject<DataFile>(dbJson);
        }

        public void SaveDataFile(string fileLocation, DataFile dataFile)
        {
            var text = JsonConvert.SerializeObject(dataFile);
            File.WriteAllText(fileLocation, text);
        }
    }
}
