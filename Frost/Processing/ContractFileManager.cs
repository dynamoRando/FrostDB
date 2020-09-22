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
        public void SaveContract(Contract contract, string contractFolder, string contractExtension)
        {

            if (!Directory.Exists(contractFolder))
            {
                Directory.CreateDirectory(contractFolder);
            }

            string fileLocation = Path.Combine(contractFolder, contract.DatabaseName + contractExtension);

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

            Console.WriteLine("Saving contract file to disk");

            _locker.ExitWriteLock();
        }

        public List<Contract> GetContracts(string contractFolder)
        {
            var contracts = new List<Contract>();

            if (!Directory.Exists(contractFolder))
            {
                Directory.CreateDirectory(contractFolder);
            }

            foreach (var file in Directory.GetFiles(contractFolder))
            {
                contracts.Add(GetContractFromDisk(file));
            }

            return contracts;
        }
        #endregion

        #region Private Methods
        private Contract GetContractFromDisk(string contractFile)
        {
            var dbJson = File.ReadAllText(contractFile);

            return JsonConvert.DeserializeObject<Contract>(dbJson, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore,
            });
        }
        #endregion


    }
}
