using FrostCommon.Net;
using FrostDB.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IQuery
    {
        public string DatabaseName { get; set; }
        public string TableName { get; set; }
        public Process Process { get; set; }
        public bool IsValid(string statement);
        public FrostPromptResponse Execute();
    }
}
