using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IFrostObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
