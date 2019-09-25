using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IConfiguration
    {
        public string FileLocation { get; set; }
        public string DatabaseFolder { get; set; }
        public ILocation GetLocation();
    }
}
