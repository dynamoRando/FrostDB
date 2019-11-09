using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FrostDB.Base
{
    [Serializable]
    public class BaseTable : 
        IBaseTable, ISerializable, IFrostObjectGet, IDBObject
    {
        #region Private Fields
        #endregion

        #region Public Properties
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        protected BaseTable(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }

        public Guid? Id => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();
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
