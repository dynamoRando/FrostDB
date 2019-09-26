using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
{
    public class MessageProcessor : IMessageProcessor, IProcessor
    {
        #region Private Fields
        private DataMessageProcessor _dataProcessor;
        private ContractMessageProcessor _contractProcessor;
        #endregion

        #region Public Properties
        #endregion

        #region Events
        #endregion

        #region Constructors
        public MessageProcessor()
        {
            _dataProcessor = new DataMessageProcessor();
            _contractProcessor = new ContractMessageProcessor();
        }
        #endregion

        #region Public Methods
        public void Process(IMessage message)
        {
            // switch on message type, route to appropriate X processor (data, contract, etc.)
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        #endregion

    }
}
