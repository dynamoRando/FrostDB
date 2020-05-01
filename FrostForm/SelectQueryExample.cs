using System;
using System.Collections.Generic;
using System.Text;

namespace FrostForm
{
    class SelectQueryExample : IQueryExample
    {
        public string Name => "Select";

        public string GetExample()
        {
            return @"select { * } from { testtable };";
        }
    }
}
