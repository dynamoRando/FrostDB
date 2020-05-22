using System;
using System.Collections.Generic;
using System.Text;
using FrostDB.Interface;

namespace FrostDB.EventArgs
{
    public class ContractUpdatedEventArgs : System.EventArgs, IEventArgs
    {
        public IDatabase Database { get; set; }
    }
}
