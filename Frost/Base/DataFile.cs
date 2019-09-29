﻿using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
{
    public class DataFile : IDataFile
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
    }
}
