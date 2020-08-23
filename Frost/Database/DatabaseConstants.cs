using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class DatabaseConstants
    {
        /// <summary>
        /// Size of Page is 8k
        /// </summary>
        public const int PAGE_SIZE = 8192;

        /// <summary>
        /// Size of a GUID is 16 bytes
        /// </summary>
        public const int PARTICIPANT_ID_SIZE = 16; 
    }
}
