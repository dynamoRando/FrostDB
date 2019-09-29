using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
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
        public IMessageContent Content { get; }
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Message()
        {

        }
        public Message(ILocation destination, ILocation orgin, IMessageContent content)
        {
            CreatedDateTime = DateTime.Now;
            Destination = destination;
            Origin = orgin;
            _id = Guid.NewGuid();
            Content = content;
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion


    }
}
