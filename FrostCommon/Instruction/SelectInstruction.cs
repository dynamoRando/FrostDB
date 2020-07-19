using System;
using System.Collections.Generic;
using System.Text;

namespace FrostCommon.Instruction
{
    [Serializable]
    public class SelectInstruction : IInstruction
    {
        public string DatabaseName { get; set; }
        public string TableName { get; set; }
        public List<string> Columns { get; set; }
        public List<Condition> Conditions { get; set; }
        public List<InnerJoin> InnerJoins { get; set; }
        public List<LeftJoin> LeftJoins { get; set; }
        public List<RightJoin> RightJoins { get; set; }

        public SelectInstruction()
        {
            Conditions = new List<Condition>();
            Columns = new List<string>();
            InnerJoins = new List<InnerJoin>();
            LeftJoins = new List<LeftJoin>();
            RightJoins = new List<RightJoin>();
        }
    }
}
