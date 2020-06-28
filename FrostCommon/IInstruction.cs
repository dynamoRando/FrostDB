using System;
using System.Collections.Generic;
using System.Text;

namespace FrostCommon
{
    public interface IInstruction
    {
        string DatabaseName { get; set; }
        string TableName { get; set; }
    }
}
