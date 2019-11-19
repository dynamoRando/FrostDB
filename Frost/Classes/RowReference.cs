﻿using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Linq;

namespace FrostDB
{
    [Serializable]
    public class RowReference : 
        IBaseRowReference, ISerializable, IDBObject
    {
        #region Private Fields
        private List<Guid?> _columnIds;
        private Guid? _tableId;
        private Guid? _databaseId;
        #endregion

        #region Public Properties
        public Guid? RowId { get; set; }
        public Participant Participant { get; set; }
        public List<Guid?> ColumnIds => _columnIds;
        public Guid? TableId => _tableId;
        public Guid? DatabaseId => _databaseId;
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public RowReference() { }

        public RowReference(List<Guid?> columnIds, Guid? tableId, Participant participant, Guid? databaseId, Guid? rowId)
        {
            _columnIds = columnIds;
            _tableId = tableId;
            Participant = participant;
            _databaseId = databaseId;
            RowId = rowId;
        }
        public RowReference(List<Guid?> columnIds, Guid? tableId, Participant participant)
        {
            _columnIds = columnIds;
            _tableId = tableId;
            Participant = participant;
        }
        protected RowReference(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            _tableId = (Guid?)serializationInfo.GetValue
              ("ReferenceTableId", typeof(Guid?));
            RowId = (Guid?)serializationInfo.GetValue
                ("ReferenceRowId", typeof(Guid?));
            _databaseId = (Guid?)serializationInfo.GetValue("ReferenceDatabaseId", typeof(Guid?));
            Participant = (Participant)serializationInfo.GetValue("ReferenceParticipant"
                , typeof(Participant));
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
            info.AddValue("ReferenceParticipant", Participant, typeof(Participant));
            info.AddValue("ReferenceColumnIds", _columnIds, typeof(List<Guid?>));
        }

        public Row Get()
        {
            var row = new Row();

            if (Participant.Location.IsLocal() || Participant.IsDatabase(_databaseId))
            {
                row = ProcessReference.GetRow(DatabaseId, TableId, RowId);
            }
            else
            {
                row = Client.GetRow(Participant.Location, DatabaseId, TableId, RowId).Result;
            }

            return row;
        }
        #endregion

        #region Private Methods
        #endregion

    }
}