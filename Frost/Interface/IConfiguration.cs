using System;
using System.Collections.Generic;
using System.Text;
using FrostCommon;

namespace FrostDB.Interface
{
    public interface IConfiguration : IFrostObject
    {
        string FileLocation { get; set; }
        string DatabaseFolder { get; set; }
        string DatabaseExtension { get; set; }
        string PartialDatabaseExtension { get; set; }
        string ContractExtension { get; set; }
        string ContractFolder { get; set; }
        Location GetLocation();
        string DatabaseDirectoryFileName { get; set; }
        string SchemaFileExtension { get; set; }
        string ParticipantFileExtension { get; set; }
        string FrostBinaryDataExtension { get; set; }
        string FrostBinaryDataDirectoryExtension { get; set; }
        string FrostSystemFolder { get; set; }
        string FrostSecurityFileExtension { get; set; }
        string FrostDbIndexFileExtension { get; set; }
        string FrostDbXactFileExtension { get; set; }
    }
}
