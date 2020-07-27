using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class InsertStatement : IStatement
    {
        public List<string> Tables { get; set; }

        public InsertStatement()
        {
            Tables = new List<string>();
        }
    }
}
