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

                if (m.Action.Contains("Table"))
                {
                    HandleTableMessage(m);
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
        private void HandleTableMessage(Message message)
        {
            switch (message.Action)
            {
                case MessageConsoleAction.Table.Get_Table_Info:
                    HandleGetTableInfo(message);
                    break;
            }
        }
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
                case MessageConsoleAction.Database.Add_Table_To_Database:
                    HandleAddNewTable(message);
                    break;
                case MessageConsoleAction.Database.Remove_Table_From_Database:
                    HandleRemoveTable(message);
                    break;
            }
        }
        private void HandleRemoveTable(Message message)
        {
            var info = JsonConvert.DeserializeObject<TableInfo>(message.Content);
            var db = ProcessReference.GetDatabase(info.DatabaseName);
            db.RemoveTable(info.TableName);
        }
        
        private void HandleAddNewTable(Message message)
        {
            var info = JsonConvert.DeserializeObject<TableInfo>(message.Content);
            var db = ProcessReference.GetDatabase(info.DatabaseName);

            var columns = new List<Column>();

            foreach(var c in info.Columns)
            {
                var col = new Column(c.Item1, c.Item2);
                columns.Add(col);
            }

            var table = new Table(info.TableName, columns, db.Id);
            db.AddTable(table);
        }

        private void HandleGetTableInfo(Message message)
        {
            var databaseTable = message.TwoGuidTuple;
            var db = ProcessReference.GetDatabase(databaseTable.Item1);
            var table = ProcessReference.GetTable(databaseTable.Item1, databaseTable.Item2);

            TableInfo info = new TableInfo();
            info.TableId = table.Id;
            info.TableName = table.Name;
            info.DatabaseName = db.Name;
            info.DatabaseId = table.DatabaseId;

            table.Columns.ForEach(c => info.Columns.Add((c.Name, c.DataType)));

            Type type = info.GetType();
            string messageContent = string.Empty;

            messageContent = JsonConvert.SerializeObject(info);
            SendMessage(message, messageContent, MessageConsoleAction.Table.Get_Table_Info_Response, type);
        }

        private void SendMessage(Message message, string responseType, string action, Type type)
        {
            NetworkReference.SendMessage(BuildMessage(message.Origin, responseType, action, type, message.Id));
        }

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
                case MessageConsoleAction.Process.Add_Database:
                    HandleAddNewDatabase(message);
                    break;
                case MessageConsoleAction.Process.Remove_Datababase:
                    HandleRemoveDatabase(message);
                    break;
                default:
                    throw new NotImplementedException("Unknown message console message");
            }
        }

        private void HandleRemoveDatabase(Message message)
        {
            ProcessReference.RemoveDatabase(message.Content);
            SendMessage(message, string.Empty, MessageConsoleAction.Process.Remove_Database_Response, message.Content.GetType());
        }

        private void HandleAddNewDatabase(Message message)
        {
            ProcessReference.AddDatabase(message.Content);
            SendMessage(message, string.Empty, MessageConsoleAction.Process.Add_Database_Response, message.Content.GetType());
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

            db.Tables.ForEach(t => info.AddToTables((t.Id, t.Name)));

            Type type = info.GetType();
            string messageContent = string.Empty;

            messageContent = JsonConvert.SerializeObject(info);
            SendMessage(message, messageContent, MessageConsoleAction.Database.Get_Database_Info_Response, type);
        }

        private void HandleGetProcessId(Message message)
        {
            string messageContent = string.Empty;
            Type type = ProcessReference.Process.Id.GetType();
            messageContent = JsonConvert.SerializeObject(ProcessReference.Process.Id);

            SendMessage(message, messageContent, MessageConsoleAction.Process.Get_Id_Response, type);
        }

        private void HandleProcessGetDatabases(Message message)
        {
            string messageContent = string.Empty;

            List<string> databases = new List<string>();
            ProcessReference.Process.Databases.ForEach(d => databases.Add(d.Name));
            Type type = databases.GetType();
            messageContent = JsonConvert.SerializeObject(databases);

            SendMessage(message, messageContent, MessageConsoleAction.Process.Get_Databases_Response, type);
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
