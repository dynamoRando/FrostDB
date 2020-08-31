using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    /// <summary>
    /// The state of the B-Tree (this is a work in progress)
    /// </summary>
    public enum BTreeContainerState
    {
        Ready,
        ReadDirty,
        LockedForUpdate,
        LockedForInsert,
        LockedForDelete
    }
}
