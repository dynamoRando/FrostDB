using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IFrostObject
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }
}
