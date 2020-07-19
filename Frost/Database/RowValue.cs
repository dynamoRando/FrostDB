using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FrostDB
{
    [Serializable]
    public class RowValue : IRowValue, ISerializable
    {
        #region Private Fields
        private Guid? _columnId;
        #endregion

        #region Public Properties
        public Guid? ColumnId => _columnId;
        public Guid? RowId { get; set; }
        public object Value { get; set; }
        public string ColumnName { get; set; }
        public Type ColumnType { get; set; }
        #endregion

        #region Constructors
        public RowValue() 
        {
            Value = new object();
        }
        public RowValue(Guid? columnId) : this()
        {
            _columnId = columnId;
        }

        public RowValue(Guid? columnId, object value) : this()
        {
            _columnId = columnId;
            Value = value;
        }

        public RowValue(Guid? columnId, object value, string columnName, Type type) : this()
        {
            _columnId = columnId;
            Value = value;
            ColumnName = columnName;
            ColumnType = type;
        }

        protected RowValue(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            Value = (object)serializationInfo.
                GetValue("RowValueValue", typeof(object));
            _columnId = (Guid?)serializationInfo.GetValue
                ("RowValueColumn", typeof(Guid?));
            ColumnName = (string)serializationInfo.GetValue
               ("RowValueColumnName", typeof(string));
            ColumnType = (Type)serializationInfo.GetValue
             ("RowValueColumnType", typeof(Type));
        }

        #endregion

        #region Public Methods

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("RowValueColumn", _columnId, typeof(Guid?));
            info.AddValue("RowValueValue", Value, typeof(object));
            info.AddValue("RowValueColumnName", ColumnName, typeof(string));
            info.AddValue("RowValueColumnType", ColumnType, typeof(Type));
        }
        #endregion


    }
}
