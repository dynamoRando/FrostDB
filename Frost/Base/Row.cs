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
        public Guid Id => throw new NotImplementedException();
        public List<IColumn> Columns => throw new NotImplementedException();
        public List<IRowValue> Values => throw new NotImplementedException();

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Id", Id, typeof(Guid));
            info.AddValue("Columns", Columns, typeof(List<Column>));
            info.AddValue("Values", Values, typeof(List<IRowValue>));
        }

        protected Row(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }

        public Row()
        {
        }
    }
}
