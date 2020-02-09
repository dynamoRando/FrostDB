using FrostCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public static class MessageBuilder
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
		/// <summary>
		/// Sends a response message back to the parameters message's origin with data. This method is usually intended to send some data back to the requestor.
		/// </summary>
		/// <param name="message">The original message we are replying to</param>
		/// <param name="responseType">Message Content (string data seralized as JSON)</param>
		/// <param name="action">Uusually a CONST STRING identifying the requested action being taken</param>
		/// <param name="type">The type of the message content (parameter 2)</param>
		/// <param name="actionType">An enumeration of the type of action being requested</param>
		public static void SendResponse(Message message, string responseType, string action, Type type, MessageActionType actionType)
		{
			NetworkReference.SendMessage(BuildMessage(message.Origin, responseType, action, type, message.Id, actionType));
		}
		#endregion

		#region Private Methods
		private static Message BuildMessage(Location destination, string content, string action, Type contentType, Guid? referenceMessageId, MessageActionType actionType)
		{
			Message response = new Message(
				destination: destination,
				origin: FrostDB.Process.GetLocation(),
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
