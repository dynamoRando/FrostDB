using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FrostDB.Base
{
    [Serializable]
    public class Row : IRow, ISerializable
    {
        #region Private Fields
        private Guid? _id;
        private List<IColumn> _columns;
        private List<IRowValue> _values;
        #endregion

        public Guid? Id => _id;
        public List<IColumn> Columns => _columns;
        public List<IRowValue> Values => _values;

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Id", Id.Value, typeof(Guid));
            info.AddValue("Columns", Columns, typeof(List<IColumn>));
            info.AddValue("Values", Values, typeof(List<IRowValue>));
        }

        protected Row(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            _id = (Guid)serializationInfo.GetValue("Id", typeof(Guid));
            _values = (List<IRowValue>)serializationInfo.GetValue("Values", typeof(List<IRowValue>));
            _columns = (List<IColumn>)serializationInfo.GetValue
                ("Columns", typeof(List<IColumn>));
        }

        public Row()
        {
        }
    }
}
