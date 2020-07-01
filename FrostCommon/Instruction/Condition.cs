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
        public Guid? Id { get; set; }

        public Condition()
        {
            Id = Guid.NewGuid();
        }
    }

    /*
     * Boolean is AND/OR. Two conditions must be at the same level for this to work
     * */
}
