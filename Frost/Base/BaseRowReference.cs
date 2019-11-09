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
        private List<Guid?> _columnIds;
        private Guid? _tableId;
        #endregion

        #region Public Properties
        public Guid? RowId { get; set; }
        public Location Location { get; set; }
        public List<Guid?> ColumnIds => _columnIds;
        public Guid? TableId => _tableId;
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public BaseRowReference() { }
        public BaseRowReference(List<Guid?> columnIds, Guid? tableId, Location location)
        {
            _columnIds = columnIds;
            _tableId = tableId;
            Location = location;
        }
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
