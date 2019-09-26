using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
{
    public class DataMessage : Message
    {
        public IDBObject Data { get; set; }
    }
}
