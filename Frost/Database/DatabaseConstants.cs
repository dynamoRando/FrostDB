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
        /// the size of a Row preamble (rowId + sizeOfIsLocal)
        /// </summary>
        public const int SIZE_OF_ROW_PREAMBLE = SIZE_OF_ROW_ID + SIZE_OF_IS_LOCAL;
        /// <summary>
        /// Size of Page Id (in Page preamble) - an integer
        /// </summary>
        public const int SIZE_OF_PAGE_ID = SIZE_OF_INT;
        /// <summary>
        /// Size of of Table Id (in Page preamble) - an intgeer
        /// </summary>
        public const int SIZE_OF_TABLE_ID = SIZE_OF_INT;
        /// <summary>
        /// Size of Database Id (in page preamble) - an integer
        /// </summary>
        public const int SIZE_OF_DB_ID = SIZE_OF_INT;
        /// <summary>
        /// Size of  Bytes used (in page preamble) - an integer
        /// </summary>
        public const int SIZE_OF_TOTAL_BYTES_USED = SIZE_OF_INT;
        /// <summary>
        /// The total size of a page preamble
        /// </summary>
        public const int SIZE_OF_PAGE_PREAMBLE = SIZE_OF_PAGE_ID + SIZE_OF_TABLE_ID + SIZE_OF_DB_ID + SIZE_OF_TOTAL_BYTES_USED;

    }
}
