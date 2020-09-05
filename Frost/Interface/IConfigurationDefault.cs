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
        int DataPortNumber { get; }
        int ConsolePortNumber { get; }
        string ContractExtension { get; }
        bool ConfigFileExists();
        string DatabaseDirectoryFileName { get; }
        string SchemaFileExtension { get; }
        string ParticpantFileExtension { get; }
        string FrostBinaryDataExtension { get; }
        string FrostBinaryDataDirectoryExtension { get; }
        string FrostSystemFolder { get; }
    }
}
