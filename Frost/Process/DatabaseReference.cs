using FrostCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class DatabaseReference
    {
        public string DatabaseName { get; set; }
        public Guid? DatabaseId { get; set; }
        public Location Location { get; set; }
    }
}
