﻿using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FrostDB
{
    [Serializable]
    public class Row : IRow, ISerializable, IEquatable<Row>
    {
        #region Private Fields
        private Guid? _id;
        private Guid? _tableId;
        private List<Guid?> _columnIds;
        private List<RowValue> _values;
        #endregion

        #region Public Properties
        public Guid? Id => _id;
        public List<RowValue> Values => _values;
        public Guid? TableId => _tableId;
        public List<Guid?> ColumnIds => _columnIds;
        public DateTime LastModified { get; set; }
        public DateTime LastAccessed { get; set; }
        public DateTime CreatedDate { get; set; }
        #endregion

        #region Public Methods
        public void AddValue(Guid? columnId, object value, string columnName, Type columnType)
        {
            _values.Add(new RowValue(columnId, value, columnName, columnType));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("RowId", Id.Value, typeof(Guid));
            info.AddValue("RowColumns", _columnIds, typeof(List<Guid?>));
            info.AddValue("RowValues", _values, typeof(List<RowValue>));
            info.AddValue("RowTableId", _tableId, typeof(Guid?));
            info.AddValue("RowLastModified", LastModified, typeof(DateTime));
            info.AddValue("RowLastAccessed", LastAccessed, typeof(DateTime));
            info.AddValue("RowCreatedDate", CreatedDate, typeof(DateTime));
        }

        public bool Equals(Row other)
        {
            bool isEqual = true;

            if (other.Values.Count != this.Values.Count)
            {
                isEqual = false;
            }

            if (isEqual)
            {
                foreach (var value in other.Values)
                {
                    if (!isEqual)
                    {
                        break;
                    }
                    foreach (var thisvalue in this.Values)
                    {
                        if (isEqual)
                        {
                            if (value.ColumnName == thisvalue.ColumnName &&
                            value.ColumnType == thisvalue.ColumnType)
                            {
                                if (value.Value.ToString() != thisvalue.Value.ToString())
                                {
                                    isEqual = false;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            return isEqual;
        }
        #endregion

        #region Protected Methods
        protected Row(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            _id = (Guid)serializationInfo.GetValue("RowId", typeof(Guid));
            _values = (List<RowValue>)serializationInfo.
                GetValue("RowValues", typeof(List<RowValue>));
            _columnIds = (List<Guid?>)serializationInfo.GetValue
                ("RowColumns", typeof(List<Guid?>));
            _tableId = (Guid?)serializationInfo.GetValue
                ("RowTableId", typeof(Guid?));
            LastAccessed = (DateTime)serializationInfo.GetValue
                ("RowLastAccessed", typeof(DateTime));
            CreatedDate = (DateTime)serializationInfo.GetValue
                ("RowCreatedDate", typeof(DateTime));
            LastModified = (DateTime)serializationInfo.GetValue
                ("RowLastModified", typeof(DateTime));
        }
        #endregion

        #region Constructors
        public Row()
        {
            _columnIds = new List<Guid?>();
            _values = new List<RowValue>();
            _id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
        }

        public Row(Guid? id)
        {
            _columnIds = new List<Guid?>();
            _values = new List<RowValue>();
            _id = id;
        }

        public Row(List<Guid?> columnIds) : this()
        {
            _columnIds.AddRange(columnIds);
        }

        public Row(List<Guid?> columnIds, Guid? tableId) : this()
        {
            _columnIds.AddRange(columnIds);
            _tableId = tableId;
        }

        public Row(List<Guid?> columnIds, Guid? tableId, Guid? rowId) : this()
        {
            _columnIds.AddRange(columnIds);
            _tableId = tableId;
            _id = rowId;
        }
        #endregion
    }
}
