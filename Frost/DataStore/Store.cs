using FrostDB.Base;
using FrostDB.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.DataStore
{
    public class Store : Process
    {
        public Store() : base(ProcessType.Store)
        {
        }
    }
}
