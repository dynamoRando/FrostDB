﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface IFrostObjectGet
    {
        Guid Id { get; }
        string Name { get; }
    }
}