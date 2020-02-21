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
        private Process _process;
        #endregion

        #region Public Properties
        public int PortNumber { get; set; }
        #endregion

        #region Events
        #endregion

        #region Constructors
        public MessageDataProcessor(Process process) : base(process)
        {
            _process = process;
            _dataProcessor = new DataMessageProcessor();
            _contractProcessor = new ContractMessageProcessor(_process);
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
                        _contractProcessor.Process(m);
                    }

                    m.SendResponse(new MessageResponse(_process), _process);
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
