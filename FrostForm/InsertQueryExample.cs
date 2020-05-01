using System;
using System.Collections.Generic;
using System.Text;

namespace FrostForm
{
    class InsertQueryExample : IQueryExample
    {
        public string Name => "Insert";

        public string GetExample()
        {
            return @"
            insert into { testtable } { ( test1, test2 ) values ( 1, '2020-03-14') } for participant { local } ;

            insert into { t1 } { ( col1 ) values ( 999 )} for participant { 127.0.0.1:525 } ;"
;
        }
    }
}
