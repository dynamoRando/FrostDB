using FrostDB.EventArgs;
using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
{
    public class ParticipantManager : IParticipantManager
    {
        #region Private Fields
        private BaseDatabase _database;
        private List<Participant> _participants;
        #endregion

        #region Public Properties
        public BaseDatabase Database => _database;
        public List<Participant> Participants => _participants;
        #endregion

        #region Constructors
        public ParticipantManager(BaseDatabase baseDatabase, List<Participant> participants)
        {
            _database = baseDatabase;
            _participants = participants;
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
        #endregion

    }
}
