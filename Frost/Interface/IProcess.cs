﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IProcess
    {
        public Guid Id { get; }
        public string Name { get; }
    }
}
