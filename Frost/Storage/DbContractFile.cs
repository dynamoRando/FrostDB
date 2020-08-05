using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class DbContractFile : IStorageFile
    {
        public int VersionNumber { get; set; }
    }
}
