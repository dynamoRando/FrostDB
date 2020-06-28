using System;
using System.Collections.Generic;
using System.Text;

namespace FrostCommon.Instruction
{
    [Serializable]
    public class Condition
    {
        public string TableName { get; set; }
        public int ConditionOrder { get; set; }
        public string Statement { get; set; }
        public string Boolean { get; set; }
    }
}
