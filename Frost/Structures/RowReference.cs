﻿using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using FrostDB.Extensions;
using FrostCommon;
using FrostCommon.DataMessages;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Diagnostics;
using log4net.Util;

namespace FrostDB
{
    /// <summary>
    /// A reference to a row in a database. The RowId is the row that is being referenced.
    /// </summary>
    /// <seealso cref="FrostDB.Interface.IBaseRowReference" />
    /// <seealso cref="System.Runtime.Serialization.ISerializable" />
    /// <seealso cref="FrostDB.Interface.IDBObject" />
    [Serializable]
    public class RowReference :
        IBaseRowReference, ISerializable, IDBObject
    {
        #region Private Fields
        private List<Guid?> _columnIds;
        private Guid? _tableId;
        private Guid? _databaseId;
        private Process _process;
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

        public RowReference(List<Guid?> columnIds, Guid? tableId, Participant participant, Guid? databaseId, Guid? rowId, Process process)
        {
            _columnIds = columnIds;
            _tableId = tableId;
            Participant = participant;
            _databaseId = databaseId;
            RowId = rowId;
            _process = process;
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

        public Row Get(Process process)
        {
            if (_process is null)
            {
                _process = process;
            }

            var row = new Row();

            if (Participant.Location.IsLocal(_process) || Participant.IsDatabase(_databaseId))
            {
                row = _process.GetRow(DatabaseId, TableId, RowId);
            }
            else
            {
                row = GetRow();

            }

            foreach(var r in row.Values)
            {
                if (r.ColumnName is null)
                {
                    var column = _process.GetTable(_databaseId, _tableId).GetColumn(r.ColumnId);
                    if (column != null)
                    {
                        r.ColumnName = column.Name;
                    }
                }
            }

            return row;
        }
        #endregion

        #region Private Methods
        private Row GetRow()
        {
            Row row = new Row();
            RemoteRowInfo request = BuildRemoteRowInfo();
            string content = JsonConvert.SerializeObject(request);
            Guid? requestId = Guid.NewGuid();
            Message rowMessage = null;

            var getRowMessage = _process.Network.BuildMessage(Participant.Location, content, MessageDataAction.Process.Get_Remote_Row, MessageType.Data, requestId, MessageActionType.Table, request.GetType());
            rowMessage = _process.Network.SendMessage(getRowMessage);

            if (rowMessage != null)
            {
                if (rowMessage.Content != null)
                {
                    row = rowMessage.GetContentAs<Row>();
                }
            }

            return row;
        }

        private RemoteRowInfo BuildRemoteRowInfo()
        {
            RemoteRowInfo request = new RemoteRowInfo();
            request.DatabaseId = this.DatabaseId;
            var db = _process.GetDatabase(this.DatabaseId);
            request.DatabaseName = db.Name;
            request.TableId = this.TableId;
            request.TableName = db.GetTableName(this.TableId);
            request.RowId = this.RowId;
            return request;
        }


        #endregion

    }
}
