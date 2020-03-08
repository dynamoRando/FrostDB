using FrostCommon;
using FrostCommon.Net;
using FrostDB.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class MessageConsoleProcessorPrompt : IMessageConsoleProcessorObject
    {
        #region Private Fields
        private MessageBuilder _messageBuilder;
        private Process _process;
        #endregion

        #region Public Properties
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public MessageConsoleProcessorPrompt(Process process)
        {
            _process = process;
            _messageBuilder = new MessageBuilder(_process);
        }
        #endregion

        #region Public Methods
        public void Process(Message message)
        {
            switch (message.Action)
            {
                case MessageConsoleAction.Prompt.Execute_Command:
                    HandleExecuteCommand(message);
                    break;
            }
        }
        #endregion

        #region Private Methods
        private void HandleExecuteCommand(Message message)
        {
            string messageContent = string.Empty;

            FrostPromptResponse response = new FrostPromptResponse();
            _process.ExecuteCommand(message.Content);
            Type type = response.GetType();
            messageContent = JsonConvert.SerializeObject(response);

            _messageBuilder.SendResponse(message, messageContent, MessageConsoleAction.Prompt.Eecute_Command_Response, type, MessageActionType.Prompt);

            throw new NotImplementedException();
        }
        #endregion

    }
}
