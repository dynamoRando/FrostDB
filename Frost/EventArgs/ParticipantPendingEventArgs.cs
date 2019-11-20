using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.EventArgs
{
    public class ParticipantPendingEventArgs : System.EventArgs, IEventArgs
    {
        public Guid? DatabaseId { get; set; }
        public Participant Participant { get; set; }
    }
}
