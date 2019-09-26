using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
{
    public class DataServer : IDataServer
    {
        #region Private Fields
        private IMessageProcessor _messageProcessor;
        #endregion

        #region Public Properties
        #endregion

        #region Events
        #endregion

        #region Constructors
        public DataServer()
        {
            _messageProcessor = new MessageProcessor();
        }
        #endregion

        #region Public Methods
        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        private void ListenForConnections()
        {
            var placeholder = new Message();
            _messageProcessor.Process(placeholder);
        }
        #endregion

    }
}
