using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class ParticipantFile : IStorageFile
    {
        public int VersionNumber { get; set; }
    }
}
