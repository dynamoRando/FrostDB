using System;
using System.Collections.Generic;
using System.Text;

namespace FrostForm
{
    class UpdateQueryExample : IQueryExample
    {
        public string Name => "Update";

        public string GetExample()
        {
            return @"UPDATE { tableName } SET { col 1 = value1, col 2 = value 2 };";
        }
    }
}
