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
        private List<Participant> _participants;
        #endregion

        #region Public Properties
        public Database Database => _database;
        public List<Participant> Participants => _participants;
        #endregion

        #region Constructors
        public ParticipantManager(Database baseDatabase, List<Participant> participants)
        {
            _database = baseDatabase;
            _participants = participants;

            if (!CheckIfDatabaseIsParticipant())
            {
                AddDatabaseAsParticipant();
            }

        }
        #endregion

        #region Public Methods
        public void AddParticipant(Participant participant)
        {
            _participants.Add(participant);
            EventManager.TriggerEvent
                (EventName.Participant.Added, GetParticipantEventArgs(participant));
        }
        #endregion

        #region Private Methods
        private ParticipantAddedEventArgs GetParticipantEventArgs(Participant participant)
        {
            return new ParticipantAddedEventArgs { DatabaseId = this.Database.Id, Participant = participant };
        }

        private bool CheckIfDatabaseIsParticipant()
        {
            return _participants.Any(participant => participant.Id == Database.Id);
        }

        private void AddDatabaseAsParticipant()
        {
            _participants.Add(new Participant(Database.Id, (Location)Process.GetLocation()));
        }
        #endregion

    }
}
