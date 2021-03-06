﻿using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using FrostCommon;

namespace FrostDB.EventArgs
{
    public class MessageRecievedEventArgs : System.EventArgs, IEventArgs
    {
        public Message Message { get; set; }
        public string StringMessage { get; set; }
        public int MessageLength { get; set; }
    }
}
