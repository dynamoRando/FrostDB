using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IMessage
    {
        Guid Id { get; }
        ILocation Destination { get; }
        ILocation Origin { get; }
        DateTime CreatedDateTime { get; }
        DateTime CreatedDateTimeUTC { get; }
        Guid? ReferenceMessageId { get; set; }
        IMessageContent Content { get; }
        string MessageAction { get; set; }

        void SendResponse();
    }
}
