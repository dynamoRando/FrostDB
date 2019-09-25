using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface INetworkConfiguration
    {
        string Address { get; set; }
        int ServerPort { get; set; }
    }
}
