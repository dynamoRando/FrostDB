using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class SchemaFile : IStorageFile
    {
        public int VersionNumber { get; set; }
    }
}
