using C5;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    class BTreeContainer
    {
        public BTreeAddress Address { get; set; }
        public TreeDictionary<int, Page> Tree { get; set; }
    }
}
