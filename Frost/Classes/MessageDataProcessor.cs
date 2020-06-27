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
        }
        #endregion

        #region Public Methods

        public override IMessage Process(IMessage message)
        {
            IMessage result = new Message();
            var m = (message as Message);

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
        #endregion

    }
}
