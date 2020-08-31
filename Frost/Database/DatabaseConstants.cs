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
        /// <summary>
        /// Size of boolean flag for if a row is local or not
        /// </summary>
        public const int SIZE_OF_IS_LOCAL = 1;
        public const int SIZE_OF_ROW_ID = 4;
    }
}
