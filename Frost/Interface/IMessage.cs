using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IMessage
    {
        Guid? Id { get; }
        Location Destination { get; }
        Location Origin { get; }
        DateTime CreatedDateTime { get; }
        DateTime CreatedDateTimeUTC { get; }
        Guid? ReferenceMessageId { get; set; }
        string Content { get; }
        string Action { get; set; }
        void SendResponse();
    }
}
