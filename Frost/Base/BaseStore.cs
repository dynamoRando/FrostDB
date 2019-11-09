using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FrostDB.Base
{
    [Serializable]
    public class BaseStore : 
        IBaseStore, ISerializable, IDBObject
    {
        #region Private Fields
        private List<Row> _rows;
        private Guid? _tableId;
        #endregion

        #region Public Properties
        public Guid? TableId => _tableId;
        public List<Row> Rows => _rows;
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public BaseStore() 
        {
            _rows = new List<Row>();
        }

        protected BaseStore(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Public Methods
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        public void AddRow(Row row)
        {
            _rows.Add(row);
        }
        #endregion

        #region Private Methods
        #endregion

    }
}
