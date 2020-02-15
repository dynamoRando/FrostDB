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
        public MessageConsoleProcessor(Process process) : base()
        {
            _process = process;
            _processProcess = new MessageConsoleProcessorProcess(_process);
            _processDatabase = new MessageConsoleProcessorDatabase(_process);
            _processTable = new MessageConsoleProcessorTable(_process);
        }
        #endregion

        #region Public Methods
        public override void Process(IMessage message)
        {
            HandleProcessMessage(message);
            var m = (message as Message);

            if (m.MessageType == MessageType.Console)
            {
                // process messages from the console
                // likely to send data back to the console so it can render on it's UI
                if (m.ReferenceMessageId.Value == Guid.Empty)
                {
                    switch (m.ActionType)
                    {
                        case MessageActionType.Process:
                            _processProcess.Process(m);
                            break;
                        case MessageActionType.Database:
                            _processDatabase.Process(m);
                            break;
                        case MessageActionType.Table:
                            _processTable.Process(m);
                            break;
                    }
                    //m.SendResponse();
                }
                else
                {
                    // do nothing
                }
            }
            else
            {
                Console.WriteLine("Message data arrived on console port");
            }
        }
        #endregion
        #region Private Methods
        #endregion
    }
}
