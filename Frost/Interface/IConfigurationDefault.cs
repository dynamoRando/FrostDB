using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IConfigurationDefault
    {
        string ConfigurationFileLocation { get; }
        string DatabaseFolder { get; }
        string DatabaseExtension { get; }
        string Name { get; }
        bool ConfigFileExists();
    }
}
