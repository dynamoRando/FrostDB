﻿using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.EventArgs
{
    public class ColumnDeletedEventArgs : System.EventArgs, IEventArgs
    {
        public string DatabaseName { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
    }
}
