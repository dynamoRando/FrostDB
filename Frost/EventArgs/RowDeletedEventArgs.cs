﻿using FrostDB.Base;
using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.EventArgs
{
    class RowDeletedEventArgs : System.EventArgs, IEventArgs
    {
        public Guid? DatabaseId { get; set; }
        public IBaseTable Table { get; set; }
        public IRow Row { get; set; }

    }
}
