using FrostDB;
using System;
using System.Collections.Generic;
using System.Text;
using FrostCommon;

namespace FrostDB.Interface
{
    public interface IDbSchema
    {
        string DatabaseName { get; set; }
        Guid? DatabaseId { get; set; }
        Location Location { get; set; }
        List<TableSchema> Tables { get; set; }
        Guid? Version { get; }
    }
}
