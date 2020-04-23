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
                case MessageDataAction.Row.Save_Row:
                    response = BuildRowSaveResponse(message);
                    break;
                case MessageDataAction.Process.Get_Remote_Row:
                    response = BuildGetRemoteRowResponse(message);
                    break;
                case MessageDataAction.Process.Remote_Row_Information:
                    response = BuildRemoteRowInfoResponse(message);
                    break;
                case MessageDataAction.Row.Delete_Row:
                    response = BuildRowDeleteResponse(message);
                    break;
                case MessageDataAction.Row.Update_Row:
                    response = BuildUpdateRowReponse(message);
                    break;
                default:
                    throw new InvalidOperationException("Unknown Message To Respond To");
            }

            return response;
        }
        #endregion

        #region Private Methods
        // TO DO: Don't do this. Refactor this.
        private Message BuildUpdateRowReponse(Message message)
        {
            Message response = new Message(
           destination: message.Origin,
           origin: _process.GetLocation(),
           messageContent: string.Empty,
           messageAction: MessageDataAction.Row.Update_Row_Response,
           referenceMessageId: message.Id,
           messageType: message.MessageType
           );

            return response;

        }
        private Message BuildRowDeleteResponse(Message message)
        {
            Message response = new Message(
             destination: message.Origin,
             origin: _process.GetLocation(),
             messageContent: string.Empty,
             messageAction: MessageDataAction.Row.Delete_Row_Response,
             referenceMessageId: message.Id,
             messageType: message.MessageType
             );

            return response;
        }
        private Message BuildRemoteRowInfoResponse(Message message)
        {
            Message response = new Message(
             destination: message.Origin,
             origin: _process.GetLocation(),
             messageContent: string.Empty,
             messageAction: MessageDataAction.Process.Remote_Row_Information_Response,
             referenceMessageId: message.Id,
             messageType: message.MessageType
             );

            return response;
        }
        private Message BuildGetRemoteRowResponse(Message message)
        {
            Message response = new Message(
            destination: message.Origin,
           origin: _process.GetLocation(),
           messageContent: string.Empty,
           messageAction: MessageDataAction.Process.Get_Remote_Row_Response,
           referenceMessageId: message.Id,
           messageType: message.MessageType
           );

            return response;
        }
        private Message BuildRowSaveResponse(Message message)
        {
            Message response = new Message(
         destination: message.Origin,
         origin: _process.GetLocation(),
         messageContent: string.Empty,
         messageAction: MessageDataAction.Row.Save_Row_Response,
         referenceMessageId: message.Id,
         messageType: message.MessageType
         );

            return response;
        }
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
