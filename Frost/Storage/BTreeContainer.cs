using C5;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    /// <summary>
    /// Class that holds the address of a tree as well as the tree itself and the tree's state (future)
    /// </summary>
    class BTreeContainer
    {
        public BTreeContainerState State { get; set; }
        public BTreeAddress Address { get; set; }
        public TreeDictionary<int, Page> Tree { get; set; }
    }
}
