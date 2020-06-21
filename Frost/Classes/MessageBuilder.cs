using FrostCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class MessageBuilder
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
        public MessageBuilder(Process process)
        {
            _process = process;
        }
        #endregion

        #region Public Methods
        public Message BuildMessage(Location destination, string content, string action, Type contentType, Guid? referenceMessageId, MessageActionType actionType)
        {
            Message response = new Message(
                destination: destination,
                origin: _process.GetLocation(),
                messageContent: content,
                messageAction: action,
                messageType: MessageType.Console,
                contentType: contentType,
                messageActionType: actionType
                );

            response.ReferenceMessageId = referenceMessageId;

            return response;
        }

        public Message BuildMessage(Location destination, string messageContent, string messageAction, MessageType messageType)
        {
            return new Message(destination, _process.GetLocation(), messageContent, messageAction, messageType);
        }
        #endregion

        #region Private Methods
        
        #endregion

    }
}
