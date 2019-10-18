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
            throw new NotImplementedException();
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
