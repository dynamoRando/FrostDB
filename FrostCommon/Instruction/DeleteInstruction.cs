using System;
using System.Collections.Generic;
using System.Text;

namespace FrostCommon.Instruction
{
    public class DeleteInstruction : IInstruction
    {
        public string DatabaseName { get; set; }
        public string TableName { get; set; }
    }
}
