using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FrostCommon
{
    /// <summary>Represents a Message to be sent between FrostDb components</summary>
    [Serializable]
    public class Message : IMessage, ISerializable
    {
        #region Private Fields
        private Guid? _id;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid? Id => _id;
        /// <summary>Gets the destination.</summary>
        /// <value>The destination.</value>
        public Location Destination { get; }
        /// <summary>
        /// Gets the origin.
        /// </summary>
        /// <value>
        /// The origin.
        /// </value>
        public Location Origin { get; }
        /// <summary>
        /// Gets the created date time.
        /// </summary>
        /// <value>
        /// The created date time.
        /// </value>
        public DateTime CreatedDateTime { get; }
        /// <summary>
        /// Gets the created date time UTC.
        /// </summary>
        /// <value>
        /// The created date time UTC.
        /// </value>
        public DateTime CreatedDateTimeUTC => CreatedDateTime.ToUniversalTime();
        /// <summary>
        /// Gets or sets the reference message identifier.
        /// </summary>
        /// <value>
        /// The reference message identifier.
        /// </value>
        public Guid? ReferenceMessageId { get; set; }
        /// <summary>
        /// Gets or sets the type of the message.
        /// </summary>
        /// <value>
        /// The type of the message. Usually either Data or Console Message Types.
        /// </value>
        public MessageType MessageType { get; set; }
        /// <summary>
        /// A string representation of the data being sent. Usually a seralized JSON object.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content { get; } // can be a row, can be a contract, etc
        /// <summary>
        /// Gets or sets the two unique identifier tuple.
        /// </summary>
        /// <value>
        /// The two unique identifier tuple.
        /// </value>
        public (Guid?, Guid?) TwoGuidTuple { get; set; }
        /// <summary>
        /// A "verb" describing the action to take on the messsage. This is usually a string const in MessageDataAction.cs or MessageConsoleAction.cs
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        public string Action { get; set; } // describes what action to take on the content, see class named MessageAction
        /// <summary>
        /// Gets or sets the json data.
        /// </summary>
        /// <value>
        /// The json data.
        /// </value>
        public string JsonData { get; set; }
        /// <summary>
        /// Gets or sets the type of the content.
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        public string ContentType { get; set; }
        /// <summary>
        /// Gets or sets the type of the action.
        /// </summary>
        /// <value>
        /// The type of the action.
        /// </value>
        public MessageActionType ActionType { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance has process requestor.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has process requestor; otherwise, <c>false</c>.
        /// </value>
        public bool HasProcessRequestor { get; set; }
        /// <summary>
        /// Gets or sets the request information identifier.
        /// </summary>
        /// <value>
        /// The request information identifier.
        /// </value>
        public Guid? RequestInformationId { get; set; }
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
            Content = (string)serializationInfo.GetValue
               ("MessageContent", typeof(string));
            Action = (string)serializationInfo.GetValue("MessageAction", typeof(string));
            JsonData = (string)serializationInfo.GetValue("MessageJsonData", typeof(string));
            MessageType = (MessageType)serializationInfo.GetValue("MessageType", typeof(MessageType));
            ContentType = (string)serializationInfo.GetValue("MessageContentType", typeof(string));
            TwoGuidTuple = ((Guid?, Guid?))serializationInfo.GetValue("MessageTwoGuidTuple", typeof((Guid?, Guid?)));
            ActionType = (MessageActionType)serializationInfo.GetValue("MessageActionType", typeof(MessageActionType));
            RequestInformationId = (Guid?)serializationInfo.GetValue("MessageRequestInformationId", typeof(Guid?));
            HasProcessRequestor = (bool)serializationInfo.GetValue("MessageHasRequestor", typeof(bool));

        }
        public Message(Location destination, Location origin, string messageContent, string messageAction, MessageType messageType) : this()
        {
            CreatedDateTime = DateTime.Now;
            Destination = destination;
            Origin = origin;
            Content = messageContent;
            Action = messageAction;
            MessageType = messageType;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="destination">The destination the message is being sent to.</param>
        /// <param name="origin">Where this message originated from.</param>
        /// <param name="messageContent">Content of the message. Usually a JSON object, can be a regular string.</param>
        /// <param name="messageAction">A "verb" describing the action to take on the messsage. This is usually a string const in MessageDataAction.cs or MessageConsoleAction.cs</param>
        /// <param name="messageType">Type of the message. See MessageType.cs in FrostCommon.</param>
        /// <param name="requestorId">The requestor identifier. Used when you want to pull the responding message back to the call site.</param>
        public Message(Location destination, Location origin, string messageContent, string messageAction, MessageType messageType, Guid? requestorId) : this()
        {
            CreatedDateTime = DateTime.Now;
            Destination = destination;
            Origin = origin;
            Content = messageContent;
            Action = messageAction;
            MessageType = messageType;
            RequestInformationId = requestorId;
        }
        public Message(Location destination, Location origin, string messageContent, string messageAction, MessageType messageType, MessageActionType messageActionType) : this()
        {
            CreatedDateTime = DateTime.Now;
            Destination = destination;
            Origin = origin;
            Content = messageContent;
            Action = messageAction;
            MessageType = messageType;
            ActionType = messageActionType;
        }
        public Message(Location destination, Location origin, string messageContent, string messageAction, MessageType messageType, Type contentType, MessageActionType messageActionType) : this()
        {
            CreatedDateTime = DateTime.Now;
            Destination = destination;
            Origin = origin;
            Content = messageContent;
            Action = messageAction;
            MessageType = messageType;
            ContentType = contentType.ToString();
            ActionType = messageActionType;
        }
        public Message(Location destination, Location origin, string messageContent, string messageAction, Guid? referenceMessageId, MessageType messageType) : this()
        {
            CreatedDateTime = DateTime.Now;
            Destination = destination;
            Origin = origin;
            _id = Guid.NewGuid();
            Content = messageContent;
            Action = messageAction;
            ReferenceMessageId = referenceMessageId;
            MessageType = messageType;
        }

        public Message(Location destination, Location origin, string messageContent, string messageAction, Guid? referenceMessageId, MessageType messageType, MessageActionType messageActionType)
        {
            CreatedDateTime = DateTime.Now;
            Destination = destination;
            Origin = origin;
            _id = Guid.NewGuid();
            Content = messageContent;
            ActionType = messageActionType;
            Action = messageAction;
            ReferenceMessageId = referenceMessageId;
            MessageType = messageType;
        }
        #endregion

        #region Public Methods
        public T GetContentAs<T>()
        {
            return JsonConvert.DeserializeObject<T>(this.Content);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("MessageId", Id.Value, typeof(Guid?));
            info.AddValue("MessageDestination", Destination, typeof(Location));
            info.AddValue("MessageOrigin", Origin, typeof(Location));
            info.AddValue("MessageCreatedDateTime", CreatedDateTime, typeof(DateTime));
            info.AddValue("MessageCreatedDateTimeUTC", CreatedDateTimeUTC, typeof(DateTime));
            info.AddValue("MessageReferenceId", ReferenceMessageId.Value, typeof(Guid?));
            info.AddValue("MessageContent", Content, typeof(string));
            info.AddValue("MessageAction", Action, typeof(string));
            info.AddValue("MessageJsonData", JsonData, typeof(string));
            info.AddValue("MessageType", MessageType, typeof(MessageType));
            info.AddValue("MessageContentType", ContentType, typeof(string));
            info.AddValue("MessageTwoGuidTuple", TwoGuidTuple, typeof((Guid?, Guid?)));
            info.AddValue("MessageActionType", ActionType, typeof(MessageActionType));
            info.AddValue("MessageRequestInformationId", RequestInformationId, typeof(Guid?));
            info.AddValue("MessageHasRequestor", HasProcessRequestor, typeof(bool));
        }
        #endregion

        #region Private Methods
        #endregion


    }
}
