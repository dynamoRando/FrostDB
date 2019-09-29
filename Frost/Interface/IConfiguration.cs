using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IConfiguration : IFrostObject
    {
        string FileLocation { get; set; }
        string DatabaseFolder { get; set; }
        string DatabaseExtension { get; set; }
        ILocation GetLocation();
    }
}
