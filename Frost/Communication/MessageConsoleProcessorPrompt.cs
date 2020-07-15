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
        public IMessage Process(Message message)
        {
            IMessage result = null;
            switch (message.Action)
            {
                case MessageConsoleAction.Prompt.Execute_Command:
                    result = HandleExecuteCommand(message);
                    break;
                case MessageConsoleAction.Prompt.Get_Plan:
                    result = HandleGetPlan(message);
                    break;
                default:
                    throw new NotImplementedException("Unknown Prompt type");
                    break;
            }
            return result;
        }
        #endregion

        #region Private Methods
        private IMessage HandleGetPlan(Message message)
        {
            string messageContent = string.Empty;
            FrostPromptPlan response = new FrostPromptPlan();
            response = _process.GetPlan(message.Content);
            Type type = response.GetType();
            messageContent = JsonConvert.SerializeObject(response);

            return _messageBuilder.BuildMessage(message.Origin, messageContent, MessageConsoleAction.Prompt.Get_Plan_Response, type, message.Id, MessageActionType.Prompt);
        }
        private IMessage HandleExecuteCommand(Message message)
        {
            string messageContent = string.Empty;

            FrostPromptResponse response = new FrostPromptResponse();
            response = _process.ExecuteCommand(message.Content);
            Type type = response.GetType();
            messageContent = JsonConvert.SerializeObject(response);

            return _messageBuilder.BuildMessage(message.Origin, messageContent, MessageConsoleAction.Prompt.Execute_Command_Response, type, message.Id, MessageActionType.Prompt);
        }
        #endregion

    }
}
