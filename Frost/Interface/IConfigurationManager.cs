using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IConfigurationManager<T> where T : IProcessConfiguration
    {
        void SaveConfiguration(T config);
        T LoadConfiguration(string configFileLocation);
    }
}
