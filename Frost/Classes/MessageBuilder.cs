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
		public static void Send(Message message, string responseType, string action, Type type, MessageActionType actionType)
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
