﻿using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FrostDB
{
    [Serializable]
    public class Contract : IContract, ISerializable, IMessageContent
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
        public Guid? ProcessId { get; set; }
        public bool IsAccepted { get; set; }
        public DateTime AcceptedDateTime { get; set; }
        public DateTime SentDateTime { get; set; }
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
            ProcessId = (Guid?)serializationInfo.GetValue("ProcessId", typeof(Guid?));
            IsAccepted = (bool)serializationInfo.GetValue("ContractIsAccepted", typeof(bool));
            AcceptedDateTime = (DateTime)serializationInfo.GetValue("ContractAcceptedDateTime", typeof(DateTime));
            SentDateTime = (DateTime)serializationInfo.GetValue("ContractSentDateTime", typeof(DateTime));
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
        }
        #endregion

        #region Private Methods
        #endregion


    }
}
