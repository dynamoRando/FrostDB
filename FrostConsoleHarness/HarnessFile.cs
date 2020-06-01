using System;

namespace FrostConsoleHarness
{
    [Serializable]
    class HarnessFile
    {
        public List<FrostInstances> Instances { get; set; }
    }
}