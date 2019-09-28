using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IProcessConfiguration : IConfiguration, INetworkConfiguration
    {
        void Get(string configLocation);
    }
}
