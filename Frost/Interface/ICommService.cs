﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface ICommService
    {
        void StartServer();
        void StopServer();
    }
}
