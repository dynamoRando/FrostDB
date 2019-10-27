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

        #region Public Properties
        public Guid? Id => _id;
        public List<IColumn> Columns => _columns;
        public List<IRowValue> Values => _values;
        #endregion

        #region Public Methods
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Id", Id.Value, typeof(Guid));
            info.AddValue("Columns", Columns, typeof(List<IColumn>));
            info.AddValue("Values", Values, typeof(List<IRowValue>));
        }
        #endregion

        #region Protected Methods
        protected Row(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            _id = (Guid)serializationInfo.GetValue("Id", typeof(Guid));
            _values = (List<IRowValue>)serializationInfo.GetValue("Values", typeof(List<IRowValue>));
            _columns = (List<IColumn>)serializationInfo.GetValue
                ("Columns", typeof(List<IColumn>));
        }
        #endregion

        #region Constructors
        public Row()
        {
        }

        public Row(List<Column> columns)
        {
            columns.ForEach(c => 
            {
                _columns.Add((Column)c);
            });
        }
        #endregion
    }
}
