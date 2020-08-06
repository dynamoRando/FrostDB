using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace FrostDB.Interface
{
    public interface IStorageFile
    {
        int VersionNumber { get; set; }
        bool IsValid();
        void Load();
    }
}
