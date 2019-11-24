using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
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

        public static void Parse(Message message)
        {
            // switch on message type, route to appropriate X processor (data, contract, etc.)
            // do the appropriate thing to the message
            // DoThing(message);

            if (message.Action.Contains("Row"))
            {
                // call RowProcessor, or whatever
            }

            if (message.Action.Contains("Contract"))
            {
                ContractMessageProcessor.Process(message);
            }

            // if this is an origin message and we're not responding to a response
            if (message.ReferenceMessageId.HasValue)
            {
                if (!(message.ReferenceMessageId.Value == Guid.Empty))
                {
                    message.SendResponse();
                }
            }
        }

        #endregion

        #region Private Methods
        #endregion

    }
}
