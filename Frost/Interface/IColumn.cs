﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IColumn : IFrostObjectGet, IDBObject
    {
        public Type DataType { get; }
    }
}
