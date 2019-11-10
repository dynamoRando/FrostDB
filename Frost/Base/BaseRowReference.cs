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

        public BaseRowReference(List<Guid?> columnIds, Guid? tableId, Location location, Guid? databaseId)
        {
            _columnIds = columnIds;
            _tableId = tableId;
            Location = location;
            _databaseId = databaseId;
        }
        public BaseRowReference(List<Guid?> columnIds, Guid? tableId, Location location)
        {
            _columnIds = columnIds;
            _tableId = tableId;
            Location = location;
        }
        protected BaseRowReference(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            _tableId = (Guid?)serializationInfo.GetValue
              ("ReferenceTableId", typeof(Guid?));
            RowId = (Guid?)serializationInfo.GetValue
                ("ReferenceRowId", typeof(Guid?));
            _databaseId = (Guid?)serializationInfo.GetValue("ReferenceDatabaseId", typeof(Guid?));
            Location = (Location)serializationInfo.GetValue("ReferenceLocation", typeof(Location));
            _columnIds = (List<Guid?>)serializationInfo.GetValue
                ("ReferenceColumnIds", typeof(List<Guid?>));
        }
        #endregion

        #region Public Methods
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ReferenceTableId", _tableId, typeof(Guid?));
            info.AddValue("ReferenceRowId", RowId, typeof(Guid?));
            info.AddValue("ReferenceDatabaseId", _databaseId, typeof(Guid?));
            info.AddValue("ReferenceLocation", Location, typeof(Location));
            info.AddValue("ReferenceColumnIds", _columnIds, typeof(List<Guid?>));
        }

        public Row Get()
        {
            var row = new Row();

            if (Location.IsLocal())
            {
                row = ProcessReference.Process.GetDatabase(DatabaseId).GetTable(TableId).GetRow(this);
            }
            else
            {
                row = ProcessReference.Process.GetRemoteRow(Location, RowId);
            }

            return row;
        }
        #endregion

        #region Private Methods
        #endregion

    }
}
