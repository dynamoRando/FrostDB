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

        public void TryGetMessage(Guid? id, out Message message)
        {
            IncomingMessages.TryRemove(id, out message);
        }

        public override IMessage ProcessWithResult(IMessage message)
        {
            throw new NotImplementedException();
        }

        public override IMessage Process(IMessage message)
        {
            IMessage result = new Message();
            result = HandleProcessMessage(message);

            var m = (message as Message);

            if (HandleMessageQueue(message))
            {
                m.HasProcessRequestor = true;
            }

            // process data messages
            if (m.MessageType == MessageType.Data)
            {
                var items = message.Action.Split('.');
                var actionType = items[0];

                if (actionType.Contains("Row"))
                {
                    result = _datarowProcesor.Process(m);
                }

                if (actionType.Contains("Contract"))
                {
                    result = _contractProcessor.Process(m);
                }

                if (actionType.Contains("Process"))
                {
                    result = _processProcessor.Process(m);
                }
            }
            else
            {
                Console.WriteLine("Message console arrived on data port");
            }

            return result;
        }
        #endregion

        #region Private Methods
        private bool HandleMessageQueue(IMessage message)
        {
            // TO DO: Need to really think about what this is doing and if this is correct
            // trying to remove from queue the reference to the message to signal elsewhere that our message was recieved
            // so there is an implicit assumption that we have got data and can carry on
            // by looking at client info in case of Message Console examples (not Message Data)
            // so in Console example, when we send the response back we always populate with the reference id to let it know we have our data
            // in Message Data we've reinvented this idea but with the requestor id to try and figure out where we are at the call site
            // we should consolidate these methods if possible. Really, the Build Response on the Message Data side should not just be reply only, but also contain the information requested -
            // this is truly actually the bug. In the Message Console side, the response always contains the actual data to be sent back, which is why it works.

            // this does not at all address why sometimes the app just drops the connection or data is not returned.
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
