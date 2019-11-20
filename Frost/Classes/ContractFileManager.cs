using FrostDB.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace FrostDB
{
    class ContractFileManager : IContractFileManager
    {
        #region Private Fields
        private ReaderWriterLockSlim _locker;
        #endregion

        #region Public Properties
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public ContractFileManager()
        {
            _locker = new ReaderWriterLockSlim();
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

        public void SaveContract(Contract contract, string contractFolder, string contractExtension)
        {
            string fileLocation = contractFolder + contract.DatabaseName + contractExtension;

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
                serializer.Serialize(writer, contract, typeof(Contract));
            }

            _locker.ExitWriteLock();
        }
    }
}
