using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class DeleteStatement : IStatement
    {
        public List<string> Tables { get; set; }

        public DeleteStatement()
        {
            Tables = new List<string>();
        }
    }
}
