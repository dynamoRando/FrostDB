using System;
using System.Collections.Generic;

namespace FrostConsoleHarness
{
    [Serializable]
    class HarnessFile
    {
        public List<FrostInstance> Instances { get; set; }
    }
}