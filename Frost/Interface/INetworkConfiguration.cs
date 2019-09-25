using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface INetworkConfiguration
    {
        public string Address { get; set; }
        public int ServerPort { get; set; }
    }
}
