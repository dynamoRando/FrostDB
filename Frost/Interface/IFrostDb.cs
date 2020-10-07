using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    internal interface IFrostDb
    {
        public string Name { get; }
        public int DatabaseId { get; }

    }
}
