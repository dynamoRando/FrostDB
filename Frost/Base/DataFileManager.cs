using FrostDB.Interface;
using Newtonsoft.Json;
using System.IO;
using System.Threading;

namespace FrostDB.Base
{
    public class DataFileManager : IDataFileManager<DataFile>
    {
        #region Private Fields
        private ReaderWriterLockSlim _locker;
        #endregion

        #region Public Properties
        public ReaderWriterLockSlim State => _locker;
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public DataFileManager()
        {
            _locker = new ReaderWriterLockSlim();
        }
        #endregion

        #region Public Methods
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
            _locker.EnterWriteLock();

            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new Newtonsoft.Json.Converters.IsoDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.TypeNameHandling = TypeNameHandling.Auto;
            serializer.Formatting = Formatting.Indented;
            serializer.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            serializer.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            serializer.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;

            using (StreamWriter sw = new StreamWriter(fileLocation))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, dataFile, typeof(DataFile));
            }

            _locker.ExitWriteLock();
        }
        #endregion

        #region Private Methods
        #endregion
    }
}
