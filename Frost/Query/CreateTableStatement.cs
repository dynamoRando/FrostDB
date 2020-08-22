using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class CreateTableStatement : IDDLStatement
    {
        public string DatabaseName { get; set; } 
    }
}
