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
        private MessageDataProcessorData _dataProcessor;
        private MessageDataProcessorContract _contractProcessor;
        private MessageDataProcessorRow _datarowProcesor;
        private MessageDataProcessorProcess _processProcessor;
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
            _dataProcessor = new MessageDataProcessorData();
            _contractProcessor = new MessageDataProcessorContract(_process);
            _datarowProcesor = new MessageDataProcessorRow(_process);
            _processProcessor = new MessageDataProcessorProcess(_process);
            IncomingMessages = new ConcurrentDictionary<Guid?, Message>();
        }
        #endregion

        #region Public Methods
        public bool HasMessageId(Guid? id)
        {
            return IncomingMessages.ContainsKey(id);
        }

        public Message TryGetMessage(Guid? id, out Message message)
        {
            IncomingMessages.TryRemove(id, out message);
        }

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

            if (_process.Network.HasMessageId(message.RequestInformationId))
            {
                _process.Network.RemoveFromQueueToken(message.RequestInformationId);
                return true;
            }

            return false;
        }
        #endregion

    }
}
