using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    // TODO this was wrong, we should re-engineer this with Message class
    public class DataMessage : Message
    {
        public IDBObject Data { get; set; }
    }
}
