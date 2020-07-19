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
        private Process _process;
        #endregion

        #region Public Properties
        public int PortNumber { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public BaseMessageProcessor(Process process) 
        {
            _process = process;
        }
        #endregion

        #region Public Methods
        public virtual IMessage Process(IMessage message)
        {
            return new Message();
        }

        #endregion

        #region Private Methods
        private MessageRecievedEventArgs CreateMessageRecievedEventArgs(Message message, string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                content = string.Empty;
            }
            return new MessageRecievedEventArgs { Message = message, MessageLength = content.Length, StringMessage = content };
        }
        #endregion


    }
}
