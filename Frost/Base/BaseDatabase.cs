using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Linq;

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
        public string Name => throw new NotImplementedException();
        public Guid? Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
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

        public IBaseTable GetTable(Guid? tableId)
        {
            return _tables.Where(t => t.Id == tableId).First();
        }

        #endregion

        #region Private Methods
        #endregion

    }
}
