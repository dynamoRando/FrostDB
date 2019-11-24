using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FrostDB
{
    [Serializable]
    public class MessageContent : ISerializable
    {
        public MessageContent() { }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            
        }

        protected MessageContent(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            
        }
    }
}
