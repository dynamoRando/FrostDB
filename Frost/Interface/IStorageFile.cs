using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IStorageFile
    {
        int VersionNumber { get; set; }
    }
}
