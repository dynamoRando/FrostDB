using System;
using System.Collections.Generic;
using System.Text;
using FrostCommon;
using FrostDB.EventArgs;

namespace FrostDB
{
    public class BaseMessageProcessor : IMessageProcessor
    {
        #region Private Fields
        #endregion

        #region Public Properties
        public int PortNumber { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public BaseMessageProcessor() { }
        #endregion

        #region Public Methods
        public virtual void Process(IMessage message)
        {
            HandleProcessMessage(message);
        }

        public void HandleProcessMessage(IMessage message)
        {
            Message m = (message as Message);
            EventManager.TriggerEvent(EventName.Message.Message_Recieved, CreateMessageRecievedEventArgs(m, m.JsonData));
        }

        #endregion

        #region Private Methods
        private MessageRecievedEventArgs CreateMessageRecievedEventArgs(Message message, string content)
        {
            return new MessageRecievedEventArgs { Message = message, MessageLength = content.Length, StringMessage = content };
        }
        #endregion


    }
}
