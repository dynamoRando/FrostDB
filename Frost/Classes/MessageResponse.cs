using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class MessageResponse
    {
        public static Message Create(Message message)
        {
            MessageContent content = null;

            // figure out here based on what kind of message what to send back.
            // using the message origin, send back to the origin
            // return response message.

            Message response = new Message(message.Origin, Process.GetLocation(), content, string.Empty);
            
            throw new NotImplementedException();
        }
    }
}
