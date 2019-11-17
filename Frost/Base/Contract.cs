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
            throw new NotImplementedException();
        }
        #endregion

        #region Public Methods
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        #endregion


    }
}
