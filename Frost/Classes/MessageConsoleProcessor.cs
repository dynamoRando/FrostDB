using System;
using FrostCommon;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FrostDB
{
    public class MessageConsoleProcessor : BaseMessageProcessor
    {

        #region Private Fields
        #endregion

        #region Public Properties
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public MessageConsoleProcessor() : base()
        {

        }
        #endregion

        #region Public Methods
        public override void Process(IMessage message)
        {
            HandleProcessMessage(message);
            var m = (message as Message);

            // process messages from the console
            // likely to send data back to the console so it can render on it's UI
            if (m.ReferenceMessageId.Value == Guid.Empty)
            {
                if (m.Action.Contains("Process"))
                {
                    // call process processor, or whatever
                    HandleProcessMessage(m);
                }

                if (m.Action.Contains("Database"))
                {
                    // call database processor, or whatever
                }

                //m.SendResponse();
            }
            else
            {
                // do nothing
            }

        }
        #endregion

        #region Private Methods
        private void HandleProcessMessage(Message message)
        {
            switch (message.Action)
            {
                case MessageConsoleAction.Process.Get_Databases:
                    HandleProcessGetDatabases(message);
                    break;
                case MessageConsoleAction.Process.Get_Id:
                    HandleGetProcessId(message);
                    break;
            }
        }

        private void HandleGetProcessId(Message message)
        {
            string messageContent = string.Empty;
            messageContent = JsonConvert.SerializeObject(ProcessReference.Process.Id);

            NetworkReference.SendMessage(BuildMessage(message.Origin, messageContent, MessageConsoleAction.Process.Get_Id_Response));
        }

        private void HandleProcessGetDatabases(Message message)
        {
            string messageContent = string.Empty;

            List<string> databases = new List<string>();
            ProcessReference.Process.Databases.ForEach(d => databases.Add(d.Name));

            messageContent = JsonConvert.SerializeObject(databases);

            NetworkReference.SendMessage(BuildMessage(message.Origin, messageContent, MessageConsoleAction.Process.Get_Databases_Response)));

        }

        private Message BuildMessage(Location destination, string content, string action)
        {
            Message response = new Message(
                destination: destination,
                origin: FrostDB.Process.GetLocation(),
                messageContent: content,
                messageAction: action,
                messageType: MessageType.Console);

            return response;
        }
        #endregion


    }
}
