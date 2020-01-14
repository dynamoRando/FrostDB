using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using FrostCommon;
using FrostDB.Extensions;

namespace FrostDB
{
    public class MessageDataProcessor : BaseMessageProcessor
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
        public MessageDataProcessor() : base()
        {
            _dataProcessor = new DataMessageProcessor();
            _contractProcessor = new ContractMessageProcessor();
        }
        #endregion

        #region Public Methods

        public override void Process(IMessage message)
        {
            HandleProcessMessage(message);

            // process data messages

            var m = (message as Message);

            if (m.MessageType == MessageType.Data)
            {
                if (message.ReferenceMessageId.Value == Guid.Empty)
                {
                    if (message.Action.Contains("Row"))
                    {
                        // call RowProcessor, or whatever
                    }

                    if (message.Action.Contains("Contract"))
                    {
                        ContractMessageProcessor.Process(m);
                    }

                    m.SendResponse();
                }
                else
                {
                    // do nothing
                }
            }
            else
            {
                Console.WriteLine("Message console arrived on data port");
            }

            
        }
        #endregion

        #region Private Methods
        
        #endregion

    }
}
