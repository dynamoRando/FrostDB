using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IProcess : IFrostObjectGet
    {
        List<IDatabase> Databases { get; }
    }
}
