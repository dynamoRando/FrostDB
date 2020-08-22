using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class CreateTableStatement : FrostIDDLStatement
    {
        public string DatabaseName { get; set; } 
        public List<string> ColumnNamesAndTypes { get; set; }
        public string RawStatement { get; set; }

        public CreateTableStatement()
        {
            ColumnNamesAndTypes = new List<string>();
        }
    }
}
