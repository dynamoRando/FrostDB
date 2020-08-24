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
        public DbSchema Schema { get; set; }
        public DbSchema2 Schema2 { get; set; }
        public List<Participant> AcceptedParticipants { get; set; }
        public List<Participant> PendingParticpants { get; set; }
    }
}
