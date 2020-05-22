using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using FrostCommon;

namespace FrostDB
{
    /// <summary>
    /// An object that describes the schema and the permissions agreed to by all participants in a database.
    /// </summary>
    /// <seealso cref="System.Runtime.Serialization.ISerializable" />
    /// <seealso cref="FrostDB.Interface.IContract" />
    [Serializable]
    public class Contract : ISerializable, IContract
    {
        #region Private Fields
        private Process _process;
        #endregion

        #region Public Properties
        public string DatabaseName { get; set; }
        public Guid? DatabaseId { get; set; }
        public Location DatabaseLocation { get; set; }
        public DbSchema DatabaseSchema { get; set; }
        public string ContractDescription { get; set; }
        public Guid? ContractId { get; set; }
        public Guid? ContractVersion { get; set; }
        public Guid? ProcessId { get; set; }
        public bool IsAccepted { get; set; }
        public DateTime AcceptedDateTime { get; set; }
        public DateTime SentDateTime { get; set; }
        public List<Guid?> ParticipantTables { get; set; }
        public List<Guid?> ProcessTables { get; set; }
        public List<TableContractPermission> ContractPermissions { get; set; }
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Contract(Process process)
        {
            _process = process;
            ParticipantTables = new List<Guid?>();
            ProcessTables = new List<Guid?>();
            ContractPermissions = new List<TableContractPermission>();
        }

        public Contract(Process process, Database database)
        {
            _process = process;
            DatabaseName = database.Name;
            DatabaseId = database.Id;
            DatabaseLocation = _process.GetLocation();
            DatabaseSchema = database.Schema;

            if (ContractId is null)
            {
                ContractId = Guid.NewGuid();
            }

            if (ContractVersion is null)
            {
                ContractVersion = Guid.NewGuid();
            }

            if (ContractPermissions is null)
            {
                ContractPermissions = new List<TableContractPermission>();
            }

            ProcessId = _process.Id;

        }

        protected Contract(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            DatabaseId = (Guid?)serializationInfo.GetValue("ContractDatabaseId", typeof(Guid?));
            DatabaseName = (string)serializationInfo.GetValue("ContractDatabaseName", typeof(string));
            DatabaseLocation = (Location)serializationInfo.GetValue("ContractDatabaseLocation", typeof(Location));
            DatabaseSchema = (DbSchema)serializationInfo.GetValue("ContractDatabaseSchema", typeof(DbSchema));
            ContractDescription = (string)serializationInfo.GetValue("ContractDatabaseDescription", typeof(string));
            ContractId = (Guid?)serializationInfo.GetValue("ContractId", typeof(Guid?));
            ContractVersion = (Guid?)serializationInfo.GetValue("ContractVersion", typeof(Guid?));
            ProcessId = (Guid?)serializationInfo.GetValue("ProcessId", typeof(Guid?));
            IsAccepted = (bool)serializationInfo.GetValue("ContractIsAccepted", typeof(bool));
            AcceptedDateTime = (DateTime)serializationInfo.GetValue("ContractAcceptedDateTime", typeof(DateTime));
            SentDateTime = (DateTime)serializationInfo.GetValue("ContractSentDateTime", typeof(DateTime));
            ParticipantTables = (List<Guid?>)serializationInfo.GetValue("ContractParticipantTables", typeof(List<Guid?>));
            ProcessTables = (List<Guid?>)serializationInfo.GetValue("ContractProcessTables", typeof(List<Guid?>));
            ContractPermissions = (List<TableContractPermission>)serializationInfo.GetValue("ContractPermissions", typeof(List<TableContractPermission>));
        }
        #endregion

        #region Public Methods
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ContractDatabaseId", DatabaseId.Value, typeof(Guid?));
            info.AddValue("ContractDatabaseName", DatabaseName, typeof(string));
            info.AddValue("ContractDatabaseLocation", DatabaseLocation, typeof(Location));
            info.AddValue("ContractDatabaseSchema", DatabaseSchema, typeof(DbSchema));
            info.AddValue("ContractDatabaseDescription", ContractDescription, typeof(string));
            info.AddValue("ContractId", ContractId.Value, typeof(Guid?));
            info.AddValue("ContractVersion", ContractVersion.Value, typeof(Guid?));
            info.AddValue("ProcessId", ProcessId.Value, typeof(Guid?));
            info.AddValue("ContractIsAccepted", IsAccepted, typeof(bool));
            info.AddValue("ContractAcceptedDateTime", AcceptedDateTime, typeof(DateTime));
            info.AddValue("ContractSentDateTime", SentDateTime, typeof(DateTime));
            info.AddValue("ContractParticipantTables", ParticipantTables, typeof(List<Guid?>));
            info.AddValue("ContractProcessTables", ProcessTables, typeof(List<Guid?>));
            info.AddValue("ContractPermissions", ContractPermissions, typeof(List<TableContractPermission>));
        }
        #endregion

        #region Private Methods
        #endregion


    }
}
