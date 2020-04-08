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
        /// <summary>
        /// Sends a response message back to the parameters message's origin with data. This method is usually intended to send some data back to the requestor.
        /// </summary>
        /// <param name="message">The original message we are replying to</param>
        /// <param name="responseType">Message Content (string data seralized as JSON)</param>
        /// <param name="action">Uusually a CONST STRING identifying the requested action being taken</param>
        /// <param name="type">The type of the message content (parameter 2)</param>
        /// <param name="actionType">An enumeration of the type of action being requested</param>
        public void SendResponse(Message message, string responseType, string action, Type type, MessageActionType actionType)
        {
            _process.Network.SendMessage(BuildMessage(message.Origin, responseType, action, type, message.Id, actionType));
        }

        public Message BuildMessage(Location destination, string messageContent, string messageAction, MessageType messageType, Guid? requestorId)
        {
            return new Message(destination, _process.GetLocation(), messageContent, messageAction, messageType, requestorId);
        }
        #endregion

        #region Private Methods
        private Message BuildMessage(Location destination, string content, string action, Type contentType, Guid? referenceMessageId, MessageActionType actionType)
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
        #endregion

    }
}
