using FrostDB.Enum;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace FrostDB.Interface
{
    public interface IProcessInfo
    {
        OSPlatform OS { get; }
    }
}
