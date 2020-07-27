using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class UpdateStatement : IStatement
    {
        public List<string> Tables { get; set; }

        public UpdateStatement()
        {
            Tables = new List<string>();
        }
    }
}
