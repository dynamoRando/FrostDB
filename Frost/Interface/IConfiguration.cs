using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IConfiguration
    {
        string FileLocation { get; set; }
        string DatabaseFolder { get; set; }
        ILocation GetLocation();
    }
}
