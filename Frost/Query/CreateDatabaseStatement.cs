using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class CreateDatabaseStatement : FrostIDDLStatement
    {
        public string DatabaseName { get; set; }
        public string RawTextWithWhitespace { get; set; }
    }
}
