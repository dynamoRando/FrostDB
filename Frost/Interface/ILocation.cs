using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface ILocation
    {
        public string IpAddress { get; set; }
        public int PortNumber { get; set; }
    }
}
