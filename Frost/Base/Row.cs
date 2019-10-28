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
        private List<Column> _columns;
        private List<RowValue> _values;
        #endregion

        #region Public Properties
        public Guid? Id => _id;
        public List<Column> Columns => _columns;
        public List<RowValue> Values => _values;
        #endregion

        #region Public Methods
        public void AddValue(Column column, object value)
        {
            _values.Add(new RowValue(column, value));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("RowId", Id.Value, typeof(Guid));
            info.AddValue("RowColumns", Columns, typeof(List<Column>));
            info.AddValue("RowValues", Values, typeof(List<RowValue>));
        }
        #endregion

        #region Protected Methods
        protected Row(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            _id = (Guid)serializationInfo.GetValue("RowId", typeof(Guid));
            _values = (List<RowValue>)serializationInfo.
                GetValue("RowValues", typeof(List<RowValue>));
            _columns = (List<Column>)serializationInfo.GetValue
                ("RowColumns", typeof(List<Column>));
        }
        #endregion

        #region Constructors
        public Row()
        {
            _columns = new List<Column>();
            _values = new List<RowValue>();
            _id = Guid.NewGuid();
        }

        public Row(List<Column> columns) : this()
        {
            _columns.AddRange(columns);
        }
        #endregion
    }
}
