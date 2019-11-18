using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.EventArgs
{
    public class PendingContractAddedEventArgs : System.EventArgs, IEventArgs
    {
        public IContract Contract { get; set; }
    }
}
