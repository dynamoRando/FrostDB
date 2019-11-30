using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface INetworkConfiguration
    {
        string Address { get; set; }
        int DataServerPort { get; set; }
        int ConsoleServerPort { get; set; }
    }
}
