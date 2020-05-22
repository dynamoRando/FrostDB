using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using FrostCommon;
using System.Linq;

namespace FrostDB
{
    public class MessageDataProcessorContract 
    {
        #region Private Fields
        Process _process;
        #endregion

        #region Public Properties
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public MessageDataProcessorContract(Process process)
        {
            _process = process;
        }
        #endregion

        #region Public Methods
        public void Process(Message message)
        {
            switch (message.Action)
            {
                case MessageDataAction.Contract.Save_Pending_Contract:
                    SavePendingContract(message);
                    break;
                case MessageDataAction.Contract.Accept_Pending_Contract:
                    AcceptPendingContract(message);
                    break;
                default:
                    throw new InvalidOperationException("Unknown Contract Message");
            }
        }
        #endregion

        #region Private Methods
        private void AcceptPendingContract(Message message)
        {
            var databaseName = message.Content;
            var db = _process.GetDatabase(databaseName);
            var pendingParticipant = db.GetPendingParticipant(message.Origin.IpAddress, message.Origin.PortNumber);
            db.RemovePendingParticipant(pendingParticipant);
            db.AddParticipant(pendingParticipant);
        }
        private void SavePendingContract(Message message)
        {
            Contract contract = null;
            if (JsonExt.TryParse(message.Content, out contract))
            {
                Console.WriteLine("Saving pending contract");
                _process.AddPendingContract(contract);
            }
        }
        #endregion
    }
}
