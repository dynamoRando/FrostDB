using FrostCommon.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostCommon
{
    public static class MessageExtensions
    {
        public static MessageSkeleton ToSkeleton(this Message message)
        {
            var result = new MessageSkeleton();
            result.Text = Json.SeralizeMessage(message);
            return result;
        }

        public static Message FromSkeleton(this MessageSkeleton message) 
        {
            var result = new Message();
            result = JsonConvert.DeserializeObject<Message>(message.Text);
            return result;
        }
    }
}
