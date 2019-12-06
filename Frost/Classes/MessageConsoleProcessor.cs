using System;
using FrostCommon;
using System.Collections.Generic;
using Newtonsoft.Json;
using FrostCommon.ConsoleMessages;

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
                    HandleDatabaseMessage(m);
                }

                //m.SendResponse();
            }
            else
            {
                // do nothing
            }

        }
        #endregion
        private void HandleDatabaseMessage(Message message)
        {
            switch (message.Action)
            {
                case MessageConsoleAction.Database.Get_Database_Info:
                    HandleGetDatabaseInfo(message);
                    break;
                case MessageConsoleAction.Database.Get_Database_Tables:
                    HandleGetDatabaseTables(message);
                    break;
            }
        }

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

        private void HandleGetDatabaseTables(Message message)
        {
            throw new NotImplementedException();
        }

        private void HandleGetDatabaseInfo(Message message)
        {
            string dbName = message.Content;

            var db = ProcessReference.GetDatabase(dbName);

            DatabaseInfo info = new DatabaseInfo();

            info.Name = db.Name;
            info.Id = db.Id;

            info.Tables = new List<string>();

            db.Tables.ForEach(t => info.Tables.Add(t.Name));

            Type type = info.GetType();
            string messageContent = string.Empty;

            messageContent = JsonConvert.SerializeObject(info);
            NetworkReference.SendMessage(BuildMessage(message.Origin, messageContent, MessageConsoleAction.Database.Get_Database_Info_Response, type, message.Id));
        }

        private void HandleGetProcessId(Message message)
        {
            string messageContent = string.Empty;
            Type type = ProcessReference.Process.Id.GetType();
            messageContent = JsonConvert.SerializeObject(ProcessReference.Process.Id);

            NetworkReference.SendMessage(BuildMessage(message.Origin, messageContent, MessageConsoleAction.Process.Get_Id_Response, type, message.Id));
        }

        private void HandleProcessGetDatabases(Message message)
        {
            string messageContent = string.Empty;

            List<string> databases = new List<string>();
            ProcessReference.Process.Databases.ForEach(d => databases.Add(d.Name));
            Type type = databases.GetType();
            messageContent = JsonConvert.SerializeObject(databases);

            NetworkReference.SendMessage(BuildMessage(message.Origin, messageContent, MessageConsoleAction.Process.Get_Databases_Response, type, message.Id));

        }

        private Message BuildMessage(Location destination, string content, string action, Type contentType, Guid? referenceMessageId)
        {
            Message response = new Message(
                destination: destination,
                origin: FrostDB.Process.GetLocation(),
                messageContent: content,
                messageAction: action,
                messageType: MessageType.Console,
                contentType: contentType
                );

            response.ReferenceMessageId = referenceMessageId;

            return response;
        }
        #endregion


    }
}
