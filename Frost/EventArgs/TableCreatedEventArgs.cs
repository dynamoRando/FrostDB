﻿using FrostDB;
using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.EventArgs
{
    public class TableCreatedEventArgs : System.EventArgs, IEventArgs
    {
        public IBaseDatabase Database { get; set; }
        public Table Table { get; set; }
    }
}
