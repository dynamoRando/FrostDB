using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class MessageResponse
    {
        #region Private Fields
        #endregion

        #region Public Properties
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        public static Message Create(Message message)
        {
            Message response = null;

            switch(message.Action)
            {
                case MessageAction.Contract.Save_Pending_Contract:
                    response = BuildSaveContractMessageReceived(message);
                    break;
                default:
                    throw new InvalidOperationException("Unknown Message");
            }

            return response;
        }
        #endregion

        #region Private Methods
        private static Message BuildSaveContractMessageReceived(Message message)
        {
            Message response = new Message(
               destination: message.Origin,
               origin: Process.GetLocation(),
               content: null,
               messageAction: MessageAction.Contract.Save_Pending_Contract_Recieved,
               referenceMessageId: message.Id
               );

            return response;
        }
        #endregion


    }
}
