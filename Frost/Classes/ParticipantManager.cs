using FrostDB.EventArgs;
using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FrostDB
{
    public class ParticipantManager : IParticipantManager
    {
        #region Private Fields
        private Database _database;
        private List<Participant> _acceptedParticipants;
        private List<Participant> _pendingParticipants;
        private Process _process;
        #endregion

        #region Public Properties
        public Database Database => _database;
        public List<Participant> AcceptedParticipants => _acceptedParticipants;
        public List<Participant> PendingParticipants => _pendingParticipants;
        #endregion

        #region Constructors
        public ParticipantManager(Database baseDatabase, List<Participant> acceptedParticipants, List<Participant> pendingParticipants, Process process)
        {
            _process = process;
            _database = baseDatabase;
            _acceptedParticipants = acceptedParticipants;
            _pendingParticipants = pendingParticipants;

            if (!CheckIfDatabaseIsParticipant())
            {
                AddDatabaseAsParticipant();
            }

        }
        #endregion

        #region Public Methods
        public void AddPendingParticipant(Participant participant)
        {
            _pendingParticipants.Add(participant);
            EventManager.TriggerEvent
                (EventName.Participant.Pending, GetParticipantPendingEventArgs(participant));
        }
        public void AddParticipant(Participant participant)
        {
            _acceptedParticipants.Add(participant);
            EventManager.TriggerEvent
                (EventName.Participant.Added, GetParticipantEventArgs(participant));
        }
        #endregion

        #region Private Methods
        private ParticipantAddedEventArgs GetParticipantEventArgs(Participant participant)
        {
            return new ParticipantAddedEventArgs { DatabaseId = this.Database.Id, Participant = participant };
        }

        private ParticipantPendingEventArgs GetParticipantPendingEventArgs(Participant participant)
        {
            return new ParticipantPendingEventArgs { DatabaseId = this.Database.Id, Participant = participant };
        }

        private bool CheckIfDatabaseIsParticipant()
        {
            return _acceptedParticipants.Any(participant => participant.Id == Database.Id);
        }

        private void AddDatabaseAsParticipant()
        {
            _acceptedParticipants.Add(new Participant(Database.Id, _process.Configuration.GetLocation()));
        }
        #endregion

    }
}
