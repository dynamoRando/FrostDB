using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public enum BTreeContainerState
    {
        Ready,
        ReadDirty,
        LockedForUpdate,
        LockedForInsert,
        LockedForDelete
    }
}
