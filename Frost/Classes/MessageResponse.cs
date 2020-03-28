using System;
using FrostCommon;
namespace FrostDB
{
    public class MessageResponse
    {
        #region Private Fields
        private Process _process;
        #endregion

        #region Public Properties
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public MessageResponse(Process process)
        {
            _process = process;
        }
        #endregion

        #region Public Methods
        public Message Create(Message message)
        {
            Message response = null;

            switch (message.Action)
            {
                case MessageDataAction.Contract.Save_Pending_Contract:
                    response = BuildSaveContractMessageReceived(message);
                    break;
                case MessageDataAction.Contract.Accept_Pending_Contract:
                    response = BuildContractAcceptPendingRecieved(message);
                    break;
                case MessageDataAction.Status.Is_Online:
                    response = BuildIsOnlineResponse(message);
                    break;
                default:
                    throw new InvalidOperationException("Unknown Message");
            }

            return response;
        }
        #endregion

        #region Private Methods
        private Message BuildIsOnlineResponse(Message message)
        {
            Message response = new Message(
         destination: message.Origin,
         origin: _process.GetLocation(),
         messageContent: string.Empty,
         messageAction: MessageDataAction.Status.Is_Online_Response,
         referenceMessageId: message.Id,
         messageType: message.MessageType
         );

            return response;
        }
        private Message BuildContractAcceptPendingRecieved(Message message)
        {
            Message response = new Message(
            destination: message.Origin,
            origin: _process.GetLocation(),
            messageContent: string.Empty,
            messageAction: MessageDataAction.Contract.Accept_Pending_Contract_Recieved,
            referenceMessageId: message.Id,
            messageType: message.MessageType
            );

            return response;
        }
        private Message BuildSaveContractMessageReceived(Message message)
        {
            Message response = new Message(
               destination: message.Origin,
               origin: _process.Configuration.GetLocation(),
               messageContent: string.Empty,
               messageAction: MessageDataAction.Contract.Save_Pending_Contract_Recieved,
               referenceMessageId: message.Id,
               messageType: message.MessageType
               );

            return response;
        }
        #endregion


    }
}
