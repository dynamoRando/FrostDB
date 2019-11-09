using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface ILocation
    {
        string IpAddress { get; set; }
        int PortNumber { get; set; }
        string Url { get; set; }
        bool IsLocal();
    }
}
