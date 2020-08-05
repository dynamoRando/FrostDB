using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class DbAddressBook : IStorageFile
    {
        public int VersionNumber { get; set; }
    }
}
