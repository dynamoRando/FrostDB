using System;
using System.Collections.Generic;
using System.Text;

namespace FrostForm
{
    class DeleteQueryExample : IQueryExample
    {
        public string Name => "Delete";

        public string GetExample()
        {
            return @"DELETE FROM { t1 };";
        }
    }
}
