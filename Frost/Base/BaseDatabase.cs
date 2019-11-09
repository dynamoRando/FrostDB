using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace FrostDB.Base
{
    [Serializable]
    public class BaseDatabase : 
        IBaseDatabase, ISerializable, IFrostObjectGet, IDBObject
    {
        #region Private Fields
        private List<BaseTable> _tables;
        #endregion

        #region Public Properties
        public List<BaseTable> Tables => _tables;
        public Guid? Id => throw new NotImplementedException();
        public string Name => throw new NotImplementedException();
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public BaseDatabase()
        {
            _tables = new List<BaseTable>();
        }

        protected BaseDatabase(SerializationInfo serializationInfo, StreamingContext streamingContext)
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
