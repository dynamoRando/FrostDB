using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FrostDB.Base
{
    [Serializable]
    public class BaseRowReference : 
        IBaseRowReference, ISerializable, IDBObject
    {
        #region Private Fields
        #endregion

        #region Public Properties
        public Guid? RowId { get; set; }
        public Location Location { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        protected BaseRowReference(SerializationInfo serializationInfo, StreamingContext streamingContext)
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
