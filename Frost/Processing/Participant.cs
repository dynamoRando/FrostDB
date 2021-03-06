﻿using FrostDB.Interface;
using System;
using System.Runtime.Serialization;
using System.Linq;
using FrostDB.Enum;
using FrostCommon;
using System.Threading.Tasks;
using System.Diagnostics;

namespace FrostDB
{
    [Serializable]
    public class Participant :
        IParticipant, IFrostObjectGet, ISerializable
    {
        #region Private Fields
        private Guid? _id;
        private string _name;
        private Location _location;
        private Process _process;
        #endregion

        #region Public Properties
        public Guid? Id => _id;
        public string Name => _name;
        public Location Location => _location;
        public Contract Contract { get; set; }
        #endregion

        #region Constructors
        public Participant()
        {
            if (!_id.HasValue)
            {
                _id = Guid.NewGuid();
            }
        }

        public Participant(Guid? participantId)
        {
            _id = participantId;
        }

        public Participant(Location location) : this()
        {
            _location = location;
        }

        public Participant(Guid? participantId, Location location) : this()
        {
            _id = participantId;
            _location = location;
        }

        protected Participant(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            _id = (Guid?)serializationInfo.GetValue
           ("ParticipantId", typeof(Guid?));
            _name = (string)serializationInfo.GetValue
           ("ParticipantName", typeof(string));
            _location = (Location)serializationInfo.GetValue
           ("ParticipantLocation", typeof(Location));
        }
        #endregion

        #region Public Methods
        public void SetProcess(Process process)
        {
            _process = process;
        }
       
        // is the participant okay with the action we're doing?
        // this logic should probably live in a different class
        public bool AcceptsAction(TableAction action)
        {
            // TO DO: fix this
            return true;
        }

        public bool HasAcceptedContract(Guid? databaseId, Process process)
        {
            if (this.IsDatabase(databaseId))
            {
                return true;
            }

            if (Contract.ContractVersion == process.GetDatabase(databaseId).Contract.ContractVersion &&
            process.GetDatabase(databaseId).AcceptedParticipants.Any(participant => participant.Id == this.Id)
            && !this.IsDatabase(databaseId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsDatabase(Guid? databaseId)
        {
            if (databaseId == Id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ParticipantId", Id.Value, typeof(Guid?));
            info.AddValue("ParticipantName", Name, typeof(string));
            info.AddValue("ParticipantLocation", Location, typeof(Location));
        }
        #endregion

        #region Private Methods
        #endregion

    }
}
