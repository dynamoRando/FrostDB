using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    /// <summary>
    /// Used to track the status of a Page (being modified, etc. This is a work in progress.)
    /// </summary>
    class PageContainer
    {
        public Page Page { get; set; }

        // TO DO: Have some sort of Page state enum
    }
}
