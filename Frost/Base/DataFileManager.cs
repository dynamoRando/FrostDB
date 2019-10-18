using FrostDB.Interface;
using Newtonsoft.Json;
using System.IO;

namespace FrostDB.Base
{
    public class DataFileManager : IDataFileManager<DataFile>
    {
        public DataFile GetDataFile(string fileLocation)
        {
            var dbJson = File.ReadAllText(fileLocation);

            return JsonConvert.DeserializeObject<DataFile>(dbJson, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore,
            });
        }

        public void SaveDataFile(string fileLocation, DataFile dataFile)
        {
            /*
               Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
               serializer.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
               serializer.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
               serializer.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
               serializer.Formatting = Newtonsoft.Json.Formatting.Indented;

               using (StreamWriter sw = new StreamWriter(fileName))
               using (Newtonsoft.Json.JsonWriter writer = new Newtonsoft.Json.JsonTextWriter(sw))
               {
                serializer.Serialize(writer, obj, typeof(MyDocumentType));
               }

             */

            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.TypeNameHandling = TypeNameHandling.Auto;
            serializer.Formatting = Formatting.Indented;

            using (StreamWriter sw = new StreamWriter(fileLocation))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, dataFile, typeof(DataFile));
            }

            //var text = JsonConvert.SerializeObject(dataFile);
            //File.WriteAllText(fileLocation, text);
        }
    }
}
