using System;
using FrostCommon;
using System.Collections.Generic;
using Newtonsoft.Json;
using FrostCommon.ConsoleMessages;
using FrostDB.Interface;

namespace FrostDB
{
    // TO DO: Should have a process to info translation class
    public class MessageConsoleProcessor : BaseMessageProcessor
    {

        #region Private Fields
        IMessageConsoleProcessorObject _processProcess;
        IMessageConsoleProcessorObject _processDatabase;
        IMessageConsoleProcessorObject _processTable;
        IMessageConsoleProcessorObject _processPrompt;
        Process _process;
        #endregion

        #region Public Properties
        public int PortNumber { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public MessageConsoleProcessor(Process process) : base(process)
        {
            _process = process;
            _processProcess = new MessageConsoleProcessorProcess(_process);
            _processDatabase = new MessageConsoleProcessorDatabase(_process);
            _processTable = new MessageConsoleProcessorTable(_process);
            _processPrompt = new MessageConsoleProcessorPrompt(_process);

        }
        #endregion

        #region Public Methods
        public override IMessage Process(IMessage message)
        {
            var m = (message as Message);
            IMessage result = null;

            if (m.MessageType == MessageType.Console)
            {
                switch (m.ActionType)
                {
                    case MessageActionType.Process:
                        result = _processProcess.Process(m);
                        break;
                    case MessageActionType.Database:
                        result = _processDatabase.Process(m);
                        break;
                    case MessageActionType.Table:
                        result = _processTable.Process(m);
                        break;
                    case MessageActionType.Prompt:
                        result = _processPrompt.Process(m);
                        break;
                }
            }
            else
            {
                Console.WriteLine("Message data arrived on console port");
            }

            return result;
        }
        #endregion

        #region Private Methods
        #endregion
    }
}
