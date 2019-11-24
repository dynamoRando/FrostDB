using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class Message : IMessage
    {
        #region Private Fields
        private Guid _id;
        #endregion

        #region Public Properties
        public Guid Id => _id;
        public ILocation Destination { get; }
        public ILocation Origin { get; }
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

        }
        public Message(ILocation destination, ILocation origin, MessageContent content, string messageAction)
        {
            CreatedDateTime = DateTime.Now;
            Destination = destination;
            Origin = origin;
            _id = Guid.NewGuid();
            Content = content;
            Action = messageAction;
        }
        public Message(ILocation destination, ILocation origin, MessageContent content, string messageAction, Guid? referenceMessageId)
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
        #endregion

        #region Private Methods
        #endregion


    }
}
