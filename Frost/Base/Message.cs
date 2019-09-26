using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
{
    public class Message : IMessage
    {
        public Guid Id { get; set; }
        public Location Destination { get; set; }
        public Location Origin { get; set; }
    }
}
