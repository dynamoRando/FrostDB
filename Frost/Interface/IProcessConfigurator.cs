﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IProcessConfigurator
    {
        IProcessConfiguration GetConfiguration();
    }
}
