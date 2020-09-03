using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class DatabaseConstants
    {
        public const int SIZE_OF_INT = 4;
        public const int SIZE_OF_BOOL = 1;
        public const int SIZE_OF_GUID = 16;

        /// <summary>
        /// Size of Page is 8k
        /// </summary>
        public const int PAGE_SIZE = 8192;
        /// <summary>
        /// Size of a GUID is 16 bytes
        /// </summary>
        public const int PARTICIPANT_ID_SIZE = SIZE_OF_GUID;
        /// <summary>
        /// Size of boolean flag for if a row is local or not
        /// </summary>
        public const int SIZE_OF_IS_LOCAL = SIZE_OF_BOOL;
        /// <summary>
        /// Size of the Row Id (int32)
        /// </summary>
        public const int SIZE_OF_ROW_ID = SIZE_OF_INT;
        /// <summary>
        /// Size of the Row holder (int32)
        /// </summary>
        public const int SIZE_OF_ROW_SIZE = SIZE_OF_INT;
        /// <summary>
        /// the size of a Row preamble (rowId + sizeOfRow + sizeOfIsLocal)
        /// </summary>
        public const int SIZE_OF_ROW_PREAMBLE = SIZE_OF_ROW_ID + SIZE_OF_ROW_SIZE + SIZE_OF_IS_LOCAL;
    }
}
