using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    /// <summary>
    /// Used to load a database from disk. Contains the schema, participants, and other information. Used in DB constructor.
    /// </summary>
    public class DbFill : IDbFill
    {   
        public DbSchema2 Schema { get; set; }
        public List<Participant2> AcceptedParticipants { get; set; }
        public List<Participant2> PendingParticpants { get; set; }
    }
}
