using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FrostDB.Base
{
    [Serializable]
    public class RowValue : IRowValue, ISerializable
    {
        #region Private Fields
        private IColumn _column;
        #endregion

        #region Public Properties
        public IColumn Column => _column;
        public object Value { get; set; }
        #endregion

        #region Constructors
        public RowValue() { }
        public RowValue(IColumn column)
        {
            _column = column;
        }

        public RowValue(IColumn column, object value)
        {
            _column = column;
            Value = value;
        }

        protected RowValue(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            Value = (object)serializationInfo.
                GetValue("RowValueValue", typeof(object));
            _column = (IColumn)serializationInfo.GetValue
                ("RowValueColumn", typeof(IColumn));
        }

        #endregion

        #region Public Methods

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("RowValueColumn", _column, typeof(IColumn));
            info.AddValue("RowValueValue", Value, typeof(object));
        }
        #endregion


    }
}
