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
        string PartialDatabaseExtension { get; }
        string ContractFolder { get; }
        string Name { get; }
        string IPAddress { get; }
        int PortNumber { get; }
        string ContractExtension { get; }
        bool ConfigFileExists();
    }
}
