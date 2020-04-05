using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using FrostCommon;
using FrostDB.Extensions;
using FrostDB.Classes;
using System.Collections.Concurrent;

namespace FrostDB
{
    public class MessageDataProcessor : BaseMessageProcessor
    {
        #region Private Fields
        private DataMessageProcessor _dataProcessor;
        private ContractMessageProcessor _contractProcessor;
        private MessageDataRowProcessor _datarowProcesor;
        private ProcessMessageProcessor _processProcessor;
        private Process _process;
        #endregion

        #region Public Properties
        public int PortNumber { get; set; }
        public ConcurrentDictionary<Guid?, Message> IncomingMessages { get; set; }
        #endregion

        #region Events
        #endregion

        #region Constructors
        public MessageDataProcessor(Process process) : base(process)
        {
            _process = process;
            _dataProcessor = new DataMessageProcessor();
            _contractProcessor = new ContractMessageProcessor(_process);
            _datarowProcesor = new MessageDataRowProcessor(_process);
            _processProcessor = new ProcessMessageProcessor(_process);
            IncomingMessages = new ConcurrentDictionary<Guid?, Message>();
        }
        #endregion

        #region Public Methods

        public override void Process(IMessage message)
        {
            HandleProcessMessage(message);

            var m = (message as Message);

            if (HandleMessageQueue(message))
            {
                m.HasProcessRequestor = true;
            }

            // process data messages
            if (m.MessageType == MessageType.Data)
            {
                if (message.ReferenceMessageId.Value == Guid.Empty)
                {
                    var items = message.Action.Split('.');
                    var actionType = items[0];

                    if (actionType.Contains("Row"))
                    {
                        _datarowProcesor.Process(m);
                    }

                    if (actionType.Contains("Contract"))
                    {
                        _contractProcessor.Process(m);
                    }

                    if (actionType.Contains("Process"))
                    {
                        _processProcessor.Process(m);
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
        private bool HandleMessageQueue(IMessage message)
        {
            if (_process.Network.HasMessageId(message.ReferenceMessageId))
            {
                _process.Network.RemoveFromQueue(message.ReferenceMessageId);
                return true;
            }

            return false;
        }
        #endregion

    }
}
