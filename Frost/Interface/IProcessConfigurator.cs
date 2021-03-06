﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IProcessConfigurator<T> where T : IProcessConfiguration
    {
        T GetConfiguration();
    }
}
