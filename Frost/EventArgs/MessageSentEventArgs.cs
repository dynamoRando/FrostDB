using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.EventArgs
{
    public class MessageSentEventArgs: System.EventArgs, IEventArgs
    {
        public Message Message { get; set; }
        public string StringMessage { get; set; }
        public int MessageLength { get; set; }
    }
}
