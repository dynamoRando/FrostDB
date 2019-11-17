using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FrostDB.Base
{
    [Serializable]
    public class Contract : IContract, ISerializable
    {
        #region Private Fields
        #endregion

        #region Public Properties
        public string DatabaseName { get; set; }
        public Guid? DatabaseId { get; set; }
        public Location DatabaseLocation { get; set; }
        public DbSchema DatabaseSchema { get; set; }
        public string ContractDescription { get; set; }
        public Guid? ContractId { get; set; }
        public Guid? ContractVersion { get; set; }
        #endregion

        #region Events
        #endregion

        #region Constructors
        protected Contract(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            DatabaseId = (Guid?)serializationInfo.GetValue("ContractDatabaseId", typeof(Guid?));
            DatabaseName = (string)serializationInfo.GetValue("ContractDatabaseName", typeof(string));
            DatabaseLocation = (Location)serializationInfo.GetValue("ContractDatabaseLocation", typeof(Location));
            DatabaseSchema = (DbSchema)serializationInfo.GetValue("ContractDatabaseSchema", typeof(DbSchema));
            ContractDescription = (string)serializationInfo.GetValue("ContractDatabaseDescription", typeof(string));
            ContractId = (Guid?)serializationInfo.GetValue("ContractId", typeof(Guid?));
            ContractVersion = (Guid?)serializationInfo.GetValue("ContractVersion", typeof(Guid?));
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
        }
        #endregion

        #region Private Methods
        #endregion


    }
}
