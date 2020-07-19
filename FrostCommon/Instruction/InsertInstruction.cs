using System;
using System.Collections.Generic;
using System.Text;

namespace FrostCommon.Instruction
{
    [Serializable]
    public class InsertInstruction : IInstruction
    {
        public string DatabaseName { get; set; }
        public string TableName { get; set; }
    }
}
