using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Linq;

namespace FrostDB.Base
{
    [Serializable]
    public class BaseRowReference : 
        IBaseRowReference, ISerializable, IDBObject
    {
        #region Private Fields
        private List<Guid?> _columnIds;
        private Guid? _tableId;
        private Guid? _databaseId;
        #endregion

        #region Public Properties
        public Guid? RowId { get; set; }
        public Location Location { get; set; }
        public List<Guid?> ColumnIds => _columnIds;
        public Guid? TableId => _tableId;
        public Guid? DatabaseId => _databaseId;
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

        public Row Get()
        {
            var row = new Row();

            if (Location.IsLocal())
            {
                row = Process.GetDatabase(DatabaseId).GetTable(TableId).GetRow(this);
            }
            else
            {
                row = Process.GetRemoteRow(Location, RowId);
            }

            return row;
        }
        #endregion

        #region Private Methods
        #endregion

    }
}
