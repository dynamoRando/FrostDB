using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FrostDB
{
    [Serializable]
    public class Message : IMessage, ISerializable
    {
        #region Private Fields
        private Guid? _id;
        #endregion

        #region Public Properties
        public Guid? Id => _id;
        public Location Destination { get; }
        public Location Origin { get; }
        public DateTime CreatedDateTime { get; }
        public DateTime CreatedDateTimeUTC => CreatedDateTime.ToUniversalTime();
        public Guid? ReferenceMessageId { get; set; }
        public MessageContent Content { get; } // can be a row, can be a contract, etc
        public string Action { get; set; } // describes what action to take on the content, see class named MessageAction
        public string JsonData { get; set; }
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Message()
        {
            _id = Guid.NewGuid();

            if (!ReferenceMessageId.HasValue)
            {
                ReferenceMessageId = Guid.Empty;
            }
        }
        protected Message(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            _id = (Guid?)serializationInfo.GetValue
               ("MessageId", typeof(Guid?));
            Destination = (Location)serializationInfo.GetValue
                ("MessageDestination", typeof(Location));
            Origin = (Location)serializationInfo.GetValue
                ("MessageOrigin", typeof(Location));
            CreatedDateTime = (DateTime)serializationInfo.GetValue
                ("MessageCreatedDateTime", typeof(DateTime));
            ReferenceMessageId = (Guid?)serializationInfo.GetValue
               ("MessageReferenceId", typeof(Guid?));
            Content = (MessageContent)serializationInfo.GetValue
               ("MessageContent", typeof(MessageContent));
            Action = (string)serializationInfo.GetValue("MessageAction", typeof(string));
            JsonData = (string)serializationInfo.GetValue("MessageJsonData", typeof(string));

        }
        public Message(Location destination, Location origin, MessageContent content, string messageAction) : this()
        {
            CreatedDateTime = DateTime.Now;
            Destination = destination;
            Origin = origin;
            Content = content;
            Action = messageAction;
        }
        public Message(Location destination, Location origin, MessageContent content, string messageAction, Guid? referenceMessageId)
        {
            CreatedDateTime = DateTime.Now;
            Destination = destination;
            Origin = origin;
            _id = Guid.NewGuid();
            Content = content;
            Action = messageAction;
            ReferenceMessageId = referenceMessageId;
        }
        #endregion

        #region Public Methods
        public void SendResponse()
        {
            // once a message has been processed, generate the appropriate response message and send it
            var message = MessageResponse.Create(this);
            Client.Send((Location)message.Destination, message);

            //throw new NotImplementedException();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("MessageId", Id.Value, typeof(Guid?));
            info.AddValue("MessageDestination", Destination, typeof(Location));
            info.AddValue("MessageOrigin", Origin, typeof(Location));
            info.AddValue("MessageCreatedDateTime", CreatedDateTime, typeof(DateTime));
            info.AddValue("MessageCreatedDateTimeUTC", CreatedDateTimeUTC, typeof(DateTime));
            info.AddValue("MessageReferenceId", ReferenceMessageId.Value, typeof(Guid?));
            info.AddValue("MessageContent", Content, typeof(MessageContent));
            info.AddValue("MessageAction", Action, typeof(string));
            info.AddValue("MessageJsonData", JsonData, typeof(string));
        }
        #endregion

        #region Private Methods
        #endregion


    }
}
